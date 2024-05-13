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
using Shared.Errors;

namespace Core.Downloader.Module.Services
{
	/// <summary>
	/// Media file downloader.
	/// </summary>
	public interface IDownloader
	{
		/// <summary>
		/// Occurs when a download operation completes successfully. This event provides the unique identifier 
		/// of the completed download, allowing handlers to identify which download has concluded.
		/// </summary>
		/// <param name="guid">The <see cref="Guid"/> associated with the specific download operation.</param>
		event Action<Guid>? DownloadCompletedEvent;

		/// <summary>
		/// Occurs when a download operation fails. This event provides the unique identifier of the download attempt
		/// and an error object detailing the download failure reason.
		/// </summary>
		/// <param name="guid">The <see cref="Guid"/> associated with the specific download operation.</param>
		/// <param name="error">An <see cref="Error"/> object containing details about the cause of the download failure.</param>
		event Action<Guid, Error>? DownloadFailedEvent;

		/// <summary>
		/// Asynchronously downloads a media file (video or audio) from source based on the specified media ID and stream.
		/// </summary>
		/// <param name="requestId">A unique identifier for the download request.</param>
		/// <param name="stream">The stream configuration of media file.</param>
		/// <param name="mediaId">The media identifier from source for the media file to be downloaded.</param>
		/// <param name="outputPath">The file path where the downloaded media file will be saved.</param>
		/// <param name="progress">An optional progress reporter that provides updates on the download progress.</param>
		/// <param name="cancellationToken">A token to observe while waiting for the task to complete to support cancellation.</param>
		/// <exception cref="ArgumentNullException">Thrown if any required arguments are <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown if any string arguments are <see cref="string.Empty"/> or white-space string.</exception>
		Task DownloadMediaAsync(Guid requestId, MediaStream stream, string mediaId, string outputPath, IProgress<double>? progress = null, CancellationToken cancellationToken = default);
	}
}