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
	/// Manages ongoing downloads.
	/// </summary>
	internal interface ICurrentDownloadManager
	{
		/// <summary>
		/// Gets a list of currently downloading items.
		/// </summary>
		IReadOnlyList<IDownloadingItem> DownloadingItems { get; }

		/// <summary>
		/// Cancels a download by its identifier.
		/// </summary>
		/// <param name="id">Identifier of the download to cancel.</param>
		void CancelDownload(Guid id);

		/// <summary>
		/// Starts a download for a scheduled item.
		/// </summary>
		/// <param name="item">The scheduled download item.</param>
		/// <param name="progressEventHandler">Progress event handler.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <exception cref="ArgumentNullException">
		/// Thrown if the <paramref name="item"/>, <paramref name="item.MediaFile"/>, 
		/// <paramref name="item.MediaFile.Stream"/>, <paramref name="item.MediaFile.YouTubeId"/> is <see langword="null"/>.
		/// </exception>
		Task StartDownloadAsync(ScheduledDownload item, Action<double>? progressEventHandler = null, CancellationToken cancellationToken = default);
	}
}