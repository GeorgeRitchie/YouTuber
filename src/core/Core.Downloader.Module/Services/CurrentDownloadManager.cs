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

using Core.Downloader.Module.Configurations;
using Core.Downloader.Module.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Errors;
using Shared.Extensions;
using Shared.Helper;

namespace Core.Downloader.Module.Services
{
	/// <summary>
	/// Manages ongoing downloads.
	/// </summary>
	internal sealed class CurrentDownloadManager : ICurrentDownloadManager
	{
		private readonly ILogger<CurrentDownloadManager> _logger;
		private readonly IDownloader _downloader;
		private readonly DownloaderAppSettings _appSettings;

		private readonly List<DownloadingItem> _downloadingItems = [];

		/// <inheritdoc/>
		public IReadOnlyList<IDownloadingItem> DownloadingItems => _downloadingItems;

		/// <summary>
		/// Initializes a new instance of <see cref="CurrentDownloadManager"/>.
		/// </summary>
		/// <param name="logger">Logger for logging operations.</param>
		/// <param name="settings">Configuration settings for the downloader.</param>
		/// <param name="downloader">Downloader service responsible for file downloads.</param>
		/// <exception cref="ArgumentNullException">Thrown if any parameter is <see langword="null"/>.</exception>
		public CurrentDownloadManager(
			ILogger<CurrentDownloadManager> logger,
			IOptions<DownloaderAppSettings> settings,
			IDownloader downloader)
		{
			ArgumentNullException.ThrowIfNull(logger, nameof(logger));
			ArgumentNullException.ThrowIfNull(downloader, nameof(downloader));
			ArgumentNullException.ThrowIfNull(settings, nameof(settings));

			_logger = logger;
			_downloader = downloader;
			_appSettings = settings.Value;

			downloader.DownloadFailedEvent += DownloadFailedEventHandler;
			downloader.DownloadCompletedEvent += DownloadCompletedEventHandler;
		}

		private void DownloadCompletedEventHandler(Guid id)
		{
			var item = _downloadingItems.Single(i => i.Download.Id == id);

			_downloadingItems.Remove(item);

			// TODO do not forget to make handler for UI too, to inform user
			_logger.LogFormattedDebug($"{nameof(DownloadManager)}.{nameof(DownloadCompletedEventHandler)}",
									"Downloading record was removed due to successful completion.",
									item);
		}

		private void DownloadFailedEventHandler(Guid id, Error error)
		{
			_downloadingItems.Remove(_downloadingItems.Single(i => i.Download.Id == id));

			// TODO do not forget to make handler for UI too, to inform user
			_logger.LogFormattedDebug($"{nameof(DownloadManager)}.{nameof(DownloadFailedEventHandler)}",
									"Downloading record was removed due to failure.",
									error);
		}

		/// <inheritdoc/>
		public void CancelDownload(Guid id)
		{
			var downloadingItem = _downloadingItems.FirstOrDefault(i => i.Download.Id == id);
			if (downloadingItem != null)
			{
				downloadingItem.CancelDownloadingCommand();
				_downloadingItems.Remove(downloadingItem);

				_logger.LogFormattedDebug($"{nameof(DownloadManager)}.{nameof(CancelDownload)}",
										"Downloading was canceled by user.",
										downloadingItem);
			}
		}

		/// <inheritdoc/>
		public Task StartDownloadAsync(ScheduledDownload item,
										Action<double>? progressEventHandler = null,
										CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(item, nameof(item));

			// These are required, since FireAndForget will not provide exceptions to current thread.
			// But child thread will throw ArgumentNullException if any of these is null.
			ArgumentNullException.ThrowIfNull(item.MediaFile, nameof(item.MediaFile));
			ArgumentNullException.ThrowIfNull(item.MediaFile.Stream, nameof(item.MediaFile.Stream));
			ArgumentNullException.ThrowIfNull(item.MediaFile.YouTubeId, nameof(item.MediaFile.YouTubeId));

			var newDownloadingItem = new DownloadingItem(item, progressEventHandler);

			_downloader.DownloadMediaAsync(
							item.Id,
							item.MediaFile.Stream,
							item.MediaFile.YouTubeId,
							GetFilePath(item),
							new Progress<double>(newDownloadingItem.ProgressChangedCommand),
							newDownloadingItem.CancellationToken)
						.FireAndForget();

			_downloadingItems.Add(newDownloadingItem);

			_logger.LogFormattedDebug($"{nameof(DownloadManager)}.{nameof(StartDownloadAsync)}",
									"Downloading was started.",
									newDownloadingItem);

			return Task.CompletedTask;
		}

		private string GetFilePath(ScheduledDownload item)
		{
			ArgumentNullException.ThrowIfNull(item, nameof(item));

			var directory = _appSettings.DownloadedFilesDirectory;

			if (item.PlayList != null)
				directory = Path.Combine(directory, item.PlayList.Title);

			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);

			string? rawFileName = item.MediaFile.Title + "." + item.MediaFile.Stream.Container;

			var fileName = FileNameFormatter.CreateUniqueSafeFileName(directory, rawFileName);

			return Path.Combine(directory, fileName);
		}
	}
}