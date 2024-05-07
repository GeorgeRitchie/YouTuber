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

namespace Core.Downloader.Module.Entities
{
	/// <summary>
	/// Represents an item currently downloading.
	/// </summary>
	internal sealed class DownloadingItem : IDownloadingItem
	{
		private readonly CancellationTokenSource cancellationTokenSource;

		/// <summary>
		/// Gets cancelation token used to cancel downloading.
		/// </summary>
		public CancellationToken CancellationToken => cancellationTokenSource.Token;

		/// <inheritdoc/>
		public ScheduledDownload Download { get; private init; }

		/// <inheritdoc/>
		public event Action<double>? ProgressChangedEvent;

		/// <summary>
		/// Initiates new instance of <see cref="DownloadingItem"/>.
		/// </summary>
		/// <param name="download">Information about item to download.</param>
		/// <param name="progressEventHandler">Event handler to track downloading process.</param>
		public DownloadingItem(ScheduledDownload download, Action<double>? progressEventHandler = null)
		{
			ArgumentNullException.ThrowIfNull(download, nameof(download));

			Download = download;
			cancellationTokenSource = new CancellationTokenSource();
			ProgressChangedEvent = progressEventHandler;
		}

		/// <summary>
		/// Triggers the <see cref="ProgressChangedEvent"/> with the current download progress.
		/// </summary>
		/// <param name="progress">The progress of the download that has completed.</param>
		public void ProgressChangedCommand(double progress)
		{
			ProgressChangedEvent?.Invoke(progress);
		}

		/// <summary>
		/// Cancels the downloading process.
		/// </summary>
		public void CancelDownloadingCommand()
		{
			cancellationTokenSource.Cancel();
		}
	}
}
