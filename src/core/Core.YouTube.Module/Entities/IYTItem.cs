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
    /// Represents a YouTube item.
    /// </summary>
    public interface IYTItem
	{
		/// <summary>
		/// Id of YouTube item, generated by YouTube.
		/// </summary>
		public string Id { get; }

		/// <summary>
		/// Url of YouTube item.
		/// </summary>
		public string Url { get; }

		/// <summary>
		/// YouTube item's title.
		/// </summary>
		public string Title { get; }

		/// <summary>
		/// YouTube item's thumbnail.
		/// </summary>
		public Thumbnail? Thumbnail { get; }

		/// <summary>
		/// YouTube item's type.
		/// </summary>
		public YTItemType ItemType { get; }
	}
}
