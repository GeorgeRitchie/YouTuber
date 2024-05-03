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

using Core.Downloader.Module.ValueObjects;

namespace Core.Downloader.Module.Entities
{
	/// <summary>
	/// Represents a media file with metadata.
	/// </summary>
	public sealed class MediaFile
	{
		private MediaStream stream;

		/// <summary>
		/// Gets the unique identifier for the media file.
		/// </summary>
		public Guid Id { get; private init; }

		/// <summary>
		/// Gets the YouTube identifier for the media file.
		/// </summary>
		public string YouTubeId { get; private init; }

		/// <summary>
		/// Gets the URL of the media file.
		/// </summary>
		public string Url { get; private init; }

		/// <summary>
		/// Gets the title of the media file.
		/// </summary>
		public string Title { get; private init; }

		/// <summary>
		/// Gets the name of the author of the media file.
		/// </summary>
		public string AuthorName { get; private init; }

		/// <summary>
		/// Gets the description of the media file.
		/// </summary>
		public string Description { get; private init; }

		/// <summary>
		/// Gets the duration of the media file.
		/// </summary>
		public TimeSpan Duration { get; private init; }

		/// <summary>
		/// Gets the thumbnail image of the media file, if available.
		/// </summary>
		public Thumbnail? Thumbnail { get; private init; }

		/// <summary>
		/// Gets the identifier of the associated scheduled download.
		/// </summary>
		public Guid ScheduleId { get; private init; }

		/// <summary>
		/// Gets the associated scheduled download details for the media file.
		/// </summary>
		public ScheduledDownload Schedule { get; private init; }

		/// <summary>
		/// Gets or sets the media stream associated with this media file.
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown when value is <see langword="null"/>.</exception>
		public MediaStream Stream
		{
			get => stream;
			set
			{
				ArgumentNullException.ThrowIfNull(value, nameof(value));

				stream = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MediaFile"/> class.
		/// </summary>
		/// <remarks>Used only by EF Core.</remarks>
		private MediaFile()
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="MediaFile"/> class.
		/// </summary>
		/// <param name="youTubeId">The YouTube identifier of the media file.</param>
		/// <param name="url">The URL of the media file.</param>
		/// <param name="title">The title of the media file.</param>
		/// <param name="authorName">The author of the media file.</param>
		/// <param name="description">The description of the media file.</param>
		/// <param name="duration">The duration of the media file.</param>
		/// <param name="schedule">The scheduled download details of the media file.</param>
		/// <param name="thumbnail">The optional thumbnail of the media file.</param>
		/// <exception cref="ArgumentException">Thrown if <paramref name="youTubeId"/>, <paramref name="url"/>, or <paramref name="title"/> is <see cref="string.Empty"/> or white-space string.</exception>
		/// <exception cref="ArgumentNullException">Thrown if any nullable parameter, except <paramref name="thumbnail"/>, is <see langword="null"/>.</exception>
		public MediaFile(string youTubeId, string url, string title, string authorName, string description, TimeSpan duration, ScheduledDownload schedule, Thumbnail? thumbnail)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(youTubeId, nameof(youTubeId));
			ArgumentException.ThrowIfNullOrWhiteSpace(url, nameof(url));
			ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));

			ArgumentNullException.ThrowIfNull(authorName, nameof(authorName));
			ArgumentNullException.ThrowIfNull(description, nameof(description));
			ArgumentNullException.ThrowIfNull(schedule, nameof(schedule));

			YouTubeId = youTubeId;
			Url = url;
			Title = title;
			AuthorName = authorName;
			Description = description;
			Duration = duration;
			Schedule = schedule;
			ScheduleId = schedule.Id;
			Thumbnail = thumbnail;

			Id = Guid.NewGuid();
		}
	}
}
