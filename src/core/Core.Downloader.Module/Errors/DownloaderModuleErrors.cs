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

using Core.Downloader.Module.Services;
using Shared.Errors;

namespace Core.Downloader.Module.Errors
{
	/// <summary>
	/// Represents a wrapper for all errors in Downloader module.
	/// </summary>
	public static class DownloaderModuleErrors
	{
		/// <summary>
		/// Represents a wrapper for all errors of <see cref="IDownloader"/> in Downloader module.
		/// </summary>
		public static class Downloader
		{
			/// <summary>
			/// Represents a download failure error.
			/// </summary>
			/// <param name="message">Message of error.</param>
			/// <returns>An instance of <see cref="Error"/>.</returns>
			public static Error DownloadingFailureError(string message) => new Error("DownloadFailed", message);

			/// <summary>
			/// Represents a stream not found in source error.
			/// </summary>
			/// <param name="sourceName">Source name where stream was searched.</param>
			/// <returns>An instance of <see cref="Error"/>.</returns>
			public static Error StreamNotFoundInYouTubeError(string sourceName) => new Error("StreamNotFound", $"Could not find requested stream in {sourceName}.");

			/// <summary>
			/// Represents a download canceled error.
			/// </summary>
			/// <returns>An instance of <see cref="Error"/>.</returns>
			public static Error DownloadCanceledError() => new Error("DownloadCanceled", "Download was canceled.");
		}
	}
}
