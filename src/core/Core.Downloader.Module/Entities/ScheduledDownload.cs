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

using Core.Downloader.Module.Enumerations;

namespace Core.Downloader.Module.Entities
{
	/// <summary>
	/// Represents a scheduled download task.
	/// </summary>
	public sealed class ScheduledDownload
	{
		private DownloadingType downloadingType;
		private Timing timing;
		private MediaFile mediaFile;

		/// <summary>
		/// Gets the unique identifier for the scheduled download.
		/// </summary>
		public Guid Id { get; private init; } = Guid.NewGuid();

		/// <summary>
		/// Gets or sets the type of downloading.
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown if the assigned value is <see langword="null"/>.</exception>
		public DownloadingType DownloadingType
		{
			get => downloadingType;
			set
			{
				ArgumentNullException.ThrowIfNull(value, nameof(value));

				downloadingType = value;
			}
		}

		/// <summary>
		/// Gets or sets the timing details for scheduled download task.
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown if the assigned value is <see langword="null"/>.</exception>
		public Timing Timing
		{
			get => timing;
			set
			{
				ArgumentNullException.ThrowIfNull(value, nameof(value));

				timing = value;
			}
		}

		/// <summary>
		/// Gets or sets the media file associated with this scheduled download.
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown if the assigned value is <see langword="null"/>.</exception>
		public MediaFile MediaFile
		{
			get => mediaFile;
			set
			{
				ArgumentNullException.ThrowIfNull(value, nameof(value));

				mediaFile = value;
			}
		}

		/// <summary>
		/// Gets or sets the playlist associated with the media file, if applicable.
		/// </summary>
		public PlayList? PlayList { get; set; }
	}
}
