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

namespace Core.Downloader.Module.Data.Constants
{
	/// <summary>
	/// Represents the table names in the Downloader module.
	/// </summary>
	internal static class TableNames
	{
		/// <summary>
		/// The media files table.
		/// </summary>
		internal const string MediaFiles = "media_files";

		/// <summary>
		/// The media streams table.
		/// </summary>
		internal const string MediaStreams = "media_streams";

		/// <summary>
		/// The playlists table.
		/// </summary>
		internal const string PlayLists = "playlists";

		/// <summary>
		/// The scheduled downloads table.
		/// </summary>
		internal const string ScheduledDownloads = "scheduled_downloads";
	}
}
