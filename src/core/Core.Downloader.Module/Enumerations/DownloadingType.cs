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

using Shared.Primitives;

namespace Core.Downloader.Module.Enumerations
{
	/// <summary>
	/// Represents downloading types enumeration.
	/// </summary>
	public sealed class DownloadingType : Enumeration<DownloadingType>
	{
		public static DownloadingType Instant = new(nameof(Instant), 0);
		public static DownloadingType Scheduled = new(nameof(Scheduled), 1);

		/// <summary>
		/// Initializes a new instance of the <see cref="DownloadingType"/> class.
		/// </summary>
		/// <param name="name">Enumeration name.</param>
		/// <param name="value">Enumeration value.</param>
		private DownloadingType(string name, int value) : base(name, value)
		{ }
	}
}
