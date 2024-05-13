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

namespace Core.Downloader.Module.Configurations
{
	/// <summary>
	/// Represents Downloader module app settings.
	/// </summary>
	public sealed class DownloaderAppSettings
	{
		/// <summary>
		/// Gets Downloader module app settings section name.
		/// </summary>
		public const string DownloaderAppSettingsSection = "DownloaderSettings";

		/// <summary>
		/// Gets or sets database connection string.
		/// </summary>
		public string DbConnectionString { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets path for downloaded files directory.
		/// </summary>
		public string DownloadedFilesDirectory { get; set; } = string.Empty;
	}
}
