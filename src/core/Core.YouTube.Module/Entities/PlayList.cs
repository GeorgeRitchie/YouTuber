﻿/* 
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

using Core.YouTube.Module.Enumerations;
using Core.YouTube.Module.ValueObjects;

namespace Core.YouTube.Module.Entities
{
    /// <summary>
    /// Represents a YouTube playlist.
    /// </summary>
    public sealed class PlayList : IYTItem
	{
		/// <summary>
		/// Id of YouTube playlist, generated by YouTube.
		/// </summary>
		public string Id { get; init; }

		/// <summary>
		/// Url of YouTube playlist.
		/// </summary>
		public string Url { get; init; }

		/// <summary>
		/// YouTube playlist's title.
		/// </summary>
		public string Title { get; init; }

		/// <summary>
		/// YouTube playlist's author name.
		/// </summary>
		public string AutherName { get; init; }

		/// <summary>
		/// YouTube playlist's description.
		/// </summary>
		public string Description { get; init; }

		/// <summary>
		/// YouTube playlist's thumbnail.
		/// </summary>
		public Thumbnail Thumbnail { get; init; }

		/// <inheritdoc/>
		public YTItemType ItemType => YTItemType.PlayList;

		private IList<Video> videos = new List<Video>();

		/// <summary>
		/// Collection of videos of playlist.
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown when null value set.</exception>
		public IList<Video> Videos
		{
			get => videos;
			init
			{
				ArgumentNullException.ThrowIfNull(value, nameof(value));

				videos = value;
			}
		}
	}
}
