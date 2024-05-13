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

using Core.Downloader.Module.Entities;
using Core.Downloader.Module.Enumerations;
using Core.Downloader.Module.Errors;
using Microsoft.Extensions.Logging;
using Shared.Errors;
using Shared.Extensions;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace Core.Downloader.Module.Services
{
	/// <summary>
	/// YouTube media file downloader.
	/// </summary>
	internal sealed class YouTubeDownloader : IDownloader
	{
		private readonly YoutubeClient youtube;
		private readonly ILogger<YouTubeDownloader> _logger;

		/// <inheritdoc/>
		public event Action<Guid>? DownloadCompletedEvent;

		/// <inheritdoc/>
		public event Action<Guid, Error>? DownloadFailedEvent;

		/// <summary>
		/// Creates an instance of <see cref="YouTubeDownloader"/>.
		/// </summary>
		/// <param name="logger">Logger for logging.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="logger"/> is <see langword="null"/>.</exception>
		public YouTubeDownloader(ILogger<YouTubeDownloader> logger)
		{
			ArgumentNullException.ThrowIfNull(logger, nameof(logger));

			_logger = logger;
			youtube = new YoutubeClient();
		}

		/// <summary>
		/// Asynchronously downloads a media file (video or audio) from YouTube based on the specified media ID and stream.
		/// </summary>
		/// <param name="requestId">A unique identifier for the download request.</param>
		/// <param name="stream">The stream configuration of media file.</param>
		/// <param name="mediaId">The YouTube media identifier for the media file to be downloaded.</param>
		/// <param name="outputPath">The file path where the downloaded media file will be saved.</param>
		/// <param name="progress">An optional progress reporter that provides updates on the download progress.</param>
		/// <param name="cancellationToken">A token to observe while waiting for the task to complete to support cancellation.</param>
		/// <remarks>
		/// This method depends on the <see cref="YoutubeExplode"/> library for fetching and downloading media streams. Exceptions are handled internally,
		/// and appropriate events are invoked on download failure or completion.
		/// </remarks>
		/// <exception cref="ArgumentNullException">Thrown if any required arguments are <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown if any string arguments are <see cref="string.Empty"/> or white-space string.</exception>
		/// <exception cref="NotImplementedException">Thrown if the stream type is not supported.</exception>
		public async Task DownloadMediaAsync(
			Guid requestId,
			MediaStream stream,
			string mediaId,
			string outputPath,
			IProgress<double>? progress = null,
			CancellationToken cancellationToken = default
		)
		{
			ArgumentNullException.ThrowIfNull(stream, nameof(stream));
			ArgumentException.ThrowIfNullOrWhiteSpace(mediaId, nameof(mediaId));
			ArgumentException.ThrowIfNullOrWhiteSpace(outputPath, nameof(outputPath));

			try
			{
				var manifest = await youtube.Videos.Streams.GetManifestAsync(mediaId, cancellationToken);

				IStreamInfo? steamToDownload = null;

				if (stream.StreamType == StreamType.AudioOnly)
					steamToDownload = FindAudioSteam(stream, manifest?.GetAudioOnlyStreams());
				else if (stream.StreamType == StreamType.VideoOnly)
					steamToDownload = FindVideoSteam(stream, manifest?.GetVideoOnlyStreams());
				else if (stream.StreamType == StreamType.Mixed)
					steamToDownload = FindVideoSteam(stream, manifest?.GetMuxedStreams());
				else
					throw new NotImplementedException(
									$"Not implemented {nameof(DownloadMediaAsync)} for Stream type: {stream.StreamType}.");

				if (steamToDownload == null)
				{
					DownloadFailedEvent?.Invoke(requestId, DownloaderModuleErrors.Downloader.StreamNotFoundInYouTubeError("YouTube"));

					_logger.LogFormattedDebug($"{nameof(YouTubeDownloader)}.{nameof(DownloadMediaAsync)}",
												"Steam not found. Download failed.",
												new
												{
													RequestId = requestId,
													Stream = stream,
													MediaId = mediaId,
													FilePath = outputPath
												});

					return;
				}
				
				// This Module should not depend on YoutubeExplode based on architecture best practices
				// But since we use YoutubeExplode lib and there was recommendation to use provided downloader method,
				// We will deliberately follow YoutubeExplode authors' recommendation despite of architecture rules.
				await youtube.Videos.Streams.DownloadAsync(steamToDownload, outputPath, progress, cancellationToken);
				
				DownloadCompletedEvent?.Invoke(requestId);

				_logger.LogFormattedDebug($"{nameof(YouTubeDownloader)}.{nameof(DownloadMediaAsync)}",
											"Media file successfully downloaded.",
											new
											{
												RequestId = requestId,
												Stream = stream,
												MediaId = mediaId,
												FilePath = outputPath
											});
			}
			catch (OperationCanceledException)
			{
				DownloadFailedEvent?.Invoke(requestId, DownloaderModuleErrors.Downloader.DownloadCanceledError());

				_logger.LogFormattedDebug($"{nameof(YouTubeDownloader)}.{nameof(DownloadMediaAsync)}",
											"Download operation was canceled.",
											new
											{
												RequestId = requestId,
												Stream = stream,
												MediaId = mediaId,
												FilePath = outputPath
											});

				if (File.Exists(outputPath))
					File.Delete(outputPath);
			}
			catch (Exception ex) when (ex is not NotImplementedException)
			{
				DownloadFailedEvent?.Invoke(requestId,
												DownloaderModuleErrors.Downloader.DownloadingFailureError(ex.Message));

				_logger.LogFormattedError($"{nameof(YouTubeDownloader)}.{nameof(DownloadMediaAsync)}",
											"Something went wrong while downloading media",
											ex);

				if (File.Exists(outputPath))
					File.Delete(outputPath);
			}
		}

		private static IStreamInfo? FindAudioSteam<T>(MediaStream? stream, IEnumerable<T>? youtubeSteams) where T : IAudioStreamInfo
		{
			if (youtubeSteams is null || stream is null)
				return null;

			return youtubeSteams.FirstOrDefault(i => i.AudioCodec?.Split('.')[0] == stream.Container
														&& i.Bitrate.ToString() == stream.Quality
														&& i.Size.Bytes == stream.SizeInBytes);
		}

		private static IStreamInfo? FindVideoSteam<T>(MediaStream? stream, IEnumerable<T>? youtubeSteams) where T : IVideoStreamInfo
		{
			if (youtubeSteams is null || stream is null)
				return null;

			return youtubeSteams.FirstOrDefault(i => i.Container.Name == stream.Container
														&& i.VideoQuality.Label == stream.Quality
														&& i.Size.Bytes == stream.SizeInBytes);
		}
	}
}
