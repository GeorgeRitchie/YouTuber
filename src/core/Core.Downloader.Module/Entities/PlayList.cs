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
	/// Represents a playlist with metadata.
	/// </summary>
	public sealed class PlayList
	{
		/// <summary>
		/// Gets the unique identifier of the playlist.
		/// </summary>
		public Guid Id { get; private init; }

		/// <summary>
		/// Gets the YouTube identifier of the playlist.
		/// </summary>
		public string YouTubeId { get; private init; }

		/// <summary>
		/// Gets the URL of the playlist.
		/// </summary>
		public string Url { get; private init; }

		/// <summary>
		/// Gets the title of the playlist.
		/// </summary>
		public string Title { get; private init; }

		/// <summary>
		/// Gets the name of the playlist's author.
		/// </summary>
		public string AuthorName { get; private init; }

		/// <summary>
		/// Gets the thumbnail image of the playlist, if available.
		/// </summary>
		public Thumbnail? Thumbnail { get; private init; }

		/// <summary>
		/// Gets the identifier for the associated scheduled download.
		/// </summary>
		public Guid ScheduleId { get; private init; }

		/// <summary>
		/// Gets the scheduled download details for the playlist.
		/// </summary>
		public ScheduledDownload Schedule { get; private init; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayList"/> class.
		/// </summary>
		/// <remarks>Used only by EF Core.</remarks>
		private PlayList()
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayList"/> class.
		/// </summary>
		/// <param name="youTubeId">The YouTube identifier of the playlist.</param>
		/// <param name="url">The URL of the playlist.</param>
		/// <param name="title">The title of the playlist.</param>
		/// <param name="authorName">The name of the author of the playlist.</param>
		/// <param name="schedule">The scheduling download details for the playlist.</param>
		/// <param name="thumbnail">A thumbnail of the playlist (optional).</param>
		/// <exception cref="ArgumentException">Thrown if <paramref name="youTubeId"/>, <paramref name="url"/>, or <paramref name="title"/> are <see cref="string.Empty"/> or white-space string.</exception>
		/// <exception cref="ArgumentNullException">Thrown if any parameter, except <paramref name="thumbnail"/>, is <see langword="null"/>.</exception>
		public PlayList(string youTubeId, string url, string title, string authorName, ScheduledDownload schedule, Thumbnail? thumbnail)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(youTubeId, nameof(youTubeId));
			ArgumentException.ThrowIfNullOrWhiteSpace(url, nameof(url));
			ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));

			ArgumentNullException.ThrowIfNull(authorName, nameof(authorName));
			ArgumentNullException.ThrowIfNull(schedule, nameof(schedule));

			YouTubeId = youTubeId;
			Url = url;
			Title = title;
			AuthorName = authorName;

			Schedule = schedule;
			ScheduleId = schedule.Id;
			Thumbnail = thumbnail;

			Id = Guid.NewGuid();
		}
	}
}
