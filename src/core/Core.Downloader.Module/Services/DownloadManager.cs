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
using Microsoft.Extensions.Logging;
using Shared.Extensions;

namespace Core.Downloader.Module.Services
{
	/// <summary>
	/// Represents facade class for managing download items.
	/// </summary>
	/// <param name="scheduledDownloadManager">Manager of scheduled downloads.</param>
	/// <param name="currentDownloadManager">Manager of instant downloads.</param>
	/// <param name="logger">Logger for logging operations.</param>
	internal sealed class DownloadManager(
		IScheduledDownloadManager scheduledDownloadManager,
		ICurrentDownloadManager currentDownloadManager,
		ILogger<DownloadManager> logger) : IDownloadManager
	{
		private readonly ILogger<DownloadManager> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
		private readonly IScheduledDownloadManager _scheduledDownloadManager = scheduledDownloadManager ?? throw new ArgumentNullException(nameof(scheduledDownloadManager));
		private readonly ICurrentDownloadManager _currentDownloadManager = currentDownloadManager ?? throw new ArgumentNullException(nameof(currentDownloadManager));

		/// <inheritdoc/>
		public IReadOnlyList<ScheduledDownload> ScheduledItems => _scheduledDownloadManager.ScheduledItems;

		/// <inheritdoc/>
		public IReadOnlyList<IDownloadingItem> DownloadingItems => _currentDownloadManager.DownloadingItems;

		/// <inheritdoc/>
		/// <exception cref="NotImplementedException">Thrown when <paramref name="item"/> has unknown state.</exception>
		public Task InitiateDownloadingAsync(ScheduledDownload item,
												Action<double>? progressEventHandler = null,
												CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(item, nameof(item));

			if (item.DownloadingType == DownloadingType.Instant)
			{
				return _currentDownloadManager.StartDownloadAsync(item, progressEventHandler, cancellationToken);
			}
			else if (item.DownloadingType == DownloadingType.Scheduled)
			{
				return _scheduledDownloadManager.ScheduleDownloadAsync(item, cancellationToken);
			}
			else
			{
				_logger.LogFormattedError($"{nameof(DownloadManager)}.{nameof(InitiateDownloadingAsync)}",
					$"Not implemented {nameof(InitiateDownloadingAsync)} for enum value {item.DownloadingType}.",
					item);

				throw new NotImplementedException();
			}
		}

		/// <inheritdoc/>
		public Task ScheduleDownloadAsync(ScheduledDownload item, CancellationToken cancellationToken = default)
			=> _scheduledDownloadManager.ScheduleDownloadAsync(item, cancellationToken);

		/// <inheritdoc/>
		public Task CancelScheduleAsync(Guid id, CancellationToken cancellationToken = default)
			=> _scheduledDownloadManager.CancelScheduleAsync(id, cancellationToken);

		/// <inheritdoc/>
		public void CancelDownload(Guid id)
			=> _currentDownloadManager.CancelDownload(id);

		/// <inheritdoc/>
		public Task StartDownloadAsync(ScheduledDownload item,
										Action<double>? progressEventHandler = null,
										CancellationToken cancellationToken = default)
			=> _currentDownloadManager.StartDownloadAsync(item, progressEventHandler, cancellationToken);
	}
}
