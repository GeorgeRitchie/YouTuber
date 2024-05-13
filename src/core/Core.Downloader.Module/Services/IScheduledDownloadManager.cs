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
	/// Manages scheduled downloads.
	/// </summary>
	internal interface IScheduledDownloadManager
	{
		/// <summary>
		/// Gets a list of all scheduled downloads.
		/// </summary>
		IReadOnlyList<ScheduledDownload> ScheduledItems { get; }

		/// <summary>
		/// Cancels a scheduled download.
		/// </summary>
		/// <param name="id">The identifier of the download to cancel.</param>
		/// <param name="cancellationToken">Token for cancelling the operation.</param>
		Task CancelScheduleAsync(Guid id, CancellationToken cancellationToken = default);

		/// <summary>
		/// Schedules a new download.
		/// </summary>
		/// <param name="item">The download item to schedule.</param>
		/// <param name="cancellationToken">Token for cancelling the operation.</param>
		/// <exception cref="ArgumentNullException">Thrown if the <paramref name="item"/> is <see langword="null"/>.</exception>
		Task ScheduleDownloadAsync(ScheduledDownload item, CancellationToken cancellationToken = default);
	}
}