/* 
	YouTuber
	Copyright (c) 2024, Sharifjon Abdulloev.

	This program is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License, version 3.0, 
	as published by the Free Software Foundation.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License, version 3.0, for more details.

	You should have received a copy of the GNU General Public License
	along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using Core.YouTube.Module.Entities;
using Core.YouTube.Module.Enumerations;
using Core.YouTube.Module.Errors;
using Microsoft.Extensions.Logging;
using Shared.Results;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Videos.Streams;

namespace Core.YouTube.Module.Services
{
	/// <inheritdoc cref="IYouTubeService"/>
	/// <param name="logger">The logger instance used for logging.</param>
	/// <param name="youtube">YouTube API provider.</param>
	internal class YouTubeService(ILogger<YouTubeService> logger, YoutubeClient youtube) : IYouTubeService
	{
		private readonly HttpClient _httpClient = new();

		/// <inheritdoc/>
		public async Task<Result<IYTItem>> GetItemInfoAsync(string url, CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(url, nameof(url));

			Result<IYTItem> result = (await GetPlayListInfoAsync(url, cancellationToken)).Map(playList => (IYTItem)playList);

			if (result.IsSuccess == false)
				result = (await GetVideoInfoAsync(url, cancellationToken)).Map(video => (IYTItem)video);

			return result;
		}

		/// <inheritdoc/>
		public async Task<Result<PlayList>> GetPlayListInfoAsync(string url, CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(url, nameof(url));

			try
			{
				var playlist = await youtube.Playlists.GetAsync(url, cancellationToken);

				var videos = await youtube.Playlists.GetVideosAsync(url, cancellationToken);

				var result = new PlayList()
				{
					Id = playlist.Id,
					Url = playlist.Url,
					Title = playlist.Title,
					Description = playlist.Description,
					AutherName = playlist.Author?.ChannelTitle ?? string.Empty,
					Thumbnail = await GetBiggestThumbnailAsync(playlist.Thumbnails, cancellationToken),
					Videos = videos.Select(i => new Video
					{
						Id = i.Id,
						Title = i.Title,
						AutherName = i.Author?.ChannelTitle ?? string.Empty,
						Duration = i.Duration ?? new TimeSpan(),
						Url = i.Url,
						Thumbnail = GetBiggestThumbnailAsync(i.Thumbnails, cancellationToken).Result,
						Description = string.Empty,
					}).ToList(),
				};

				return Result.Success(result);
			}
			catch (ArgumentException ex) when (ex.Message.StartsWith("Invalid YouTube playlist ID or URL"))
			{
				logger.LogFormattedDebug($"{nameof(YouTubeService)}.{nameof(GetPlayListInfoAsync)}",
											$"Invalid url passed: {url}",
											ex);

				return Result.Failure<PlayList>(null,
												[YouTubeModuleErrors.InvalidUrlError($"Invalid URL. Unable to pars {url}")]);
			}
			catch (Exception ex)
			{
				logger.LogFormattedError($"{nameof(YouTubeService)}.{nameof(GetPlayListInfoAsync)}",
											$"Something went wrong while analyzing url: {url}",
											ex);

				throw;
			}
		}

		/// <inheritdoc/>
		public async Task<Result<Video>> GetVideoInfoAsync(string url, CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(url, nameof(url));

			try
			{
				var videoInfo = await youtube.Videos.GetAsync(url, cancellationToken);

				var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoInfo.Id, cancellationToken);

				var video = new Video()
				{
					Id = videoInfo.Id,
					Url = videoInfo.Url,
					Title = videoInfo.Title,
					AutherName = videoInfo.Author.ChannelTitle,
					Duration = videoInfo.Duration ?? new TimeSpan(),
					Description = videoInfo.Description,
					Streams = GetStreams(streamManifest),
					Thumbnail = await GetBiggestThumbnailAsync(videoInfo.Thumbnails, cancellationToken),
				};

				return Result.Success(video);
			}
			catch (ArgumentException ex) when (ex.Message.StartsWith("Invalid YouTube video ID or URL"))
			{
				logger.LogFormattedDebug($"{nameof(YouTubeService)}.{nameof(GetVideoInfoAsync)}",
											$"Invalid url passed: {url}",
											ex);

				return Result.Failure<Video>(null,
												[YouTubeModuleErrors.InvalidUrlError($"Invalid URL. Unable to pars {url}")]);
			}
			catch (Exception ex)
			{
				logger.LogFormattedError($"{nameof(YouTubeService)}.{nameof(GetVideoInfoAsync)}",
											$"Something went wrong while analyzing url: {url}",
											ex);

				throw;
			}
		}

		private async Task<ValueObjects.Thumbnail?> GetBiggestThumbnailAsync(IEnumerable<Thumbnail> thumbnailsList, CancellationToken cancellationToken = default)
		{
			var thumbnail = thumbnailsList?.MaxBy(i => i.Resolution.Area);

			if (thumbnail == null || string.IsNullOrWhiteSpace(thumbnail.Url))
				return null;

			return new ValueObjects.Thumbnail(thumbnail.Url, await GetImageAsByteArrayAsync(thumbnail.Url, cancellationToken));
		}

		private async Task<byte[]> GetImageAsByteArrayAsync(string? url, CancellationToken cancellationToken = default)
		{
			if (string.IsNullOrWhiteSpace(url))
				return [];

			var response = await _httpClient.GetAsync(url, cancellationToken);

			if (response.IsSuccessStatusCode == false)
				return [];

			var imageBytes = await response.Content.ReadAsByteArrayAsync(cancellationToken);

			return imageBytes;
		}

		private static List<MediaStream> GetStreams(StreamManifest? streamManifest)
		{
			if (streamManifest == null)
				return [];

			var mixedStreams = streamManifest.GetMuxedStreams();
			var videoOnlyStreams = streamManifest.GetVideoOnlyStreams();
			var audioOnlyStreams = streamManifest.GetAudioOnlyStreams();

			var streams = new List<MediaStream>();

			streams.AddRange(mixedStreams.Select(i => new MediaStream
			{
				Container = i.Container.Name,
				Quality = i.VideoQuality.Label,
				SizeInBytes = i.Size.Bytes,
				Type = StreamType.Mixed,
			}));

			streams.AddRange(videoOnlyStreams.Select(i => new MediaStream
			{
				Container = i.Container.Name,
				Quality = i.VideoQuality.Label,
				SizeInBytes = i.Size.Bytes,
				Type = StreamType.VideoOnly,
			}));

			streams.AddRange(audioOnlyStreams.Select(i => new MediaStream
			{
				Container = i.AudioCodec?.Split('.').FirstOrDefault() ?? string.Empty,
				SizeInBytes = i.Size.Bytes,
				Quality = i.Bitrate.ToString(),
				Type = StreamType.AudioOnly,
			}));

			return streams;
		}
	}
}
