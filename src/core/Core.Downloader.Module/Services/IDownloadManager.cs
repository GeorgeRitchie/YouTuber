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

namespace Core.Downloader.Module.Services
{
	/// <summary>
	/// Defines the functionality for managing downloads.
	/// </summary>
	internal interface IDownloadManager
	{
		/// <summary>
		/// Gets a list of currently downloading items.
		/// </summary>
		IReadOnlyList<IDownloadingItem> DownloadingItems { get; }

		/// <summary>
		/// Gets a list of scheduled downloads.
		/// </summary>
		IReadOnlyList<ScheduledDownload> ScheduledItems { get; }

		/// <summary>
		/// Cancels an ongoing download by its identifier.
		/// </summary>
		/// <param name="id">Identifier of the download to cancel.</param>
		void CancelDownload(Guid id);

		/// <summary>
		/// Cancels a scheduled download by its identifier.
		/// </summary>
		/// <param name="id">Identifier of the scheduled download to cancel.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		Task CancelScheduleAsync(Guid id, CancellationToken cancellationToken = default);

		/// <summary>
		/// Initiates the downloading of an item, either instant or scheduled based on item state.
		/// </summary>
		/// <param name="item">The download item to start downloading.</param>
		/// <param name="progressEventHandler">Progress event handler.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="item"/> is <see langword="null"/>.</exception>
		Task InitiateDownloadingAsync(ScheduledDownload item, Action<double>? progressEventHandler = null, CancellationToken cancellationToken = default);

		/// <summary>
		/// Schedules a download to be started later.
		/// </summary>
		/// <param name="item">The download item to schedule.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="item"/> is <see langword="null"/>.</exception>
		Task ScheduleDownloadAsync(ScheduledDownload item, CancellationToken cancellationToken = default);

		/// <summary>
		/// Starts a download for an item.
		/// </summary>
		/// <param name="item">The item to start downloading.</param>
		/// <param name="progressEventHandler">Progress event handler.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="item"/> is <see langword="null"/>.</exception>
		Task StartDownloadAsync(ScheduledDownload item, Action<double>? progressEventHandler = null, CancellationToken cancellationToken = default);
	}
}