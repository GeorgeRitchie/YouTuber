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

namespace Core.YouTube.Module.Enumerations
{
	/// <summary>
	/// Represents the YouTube item types enumeration.
	/// </summary>
	public sealed class YTItemType : Enumeration<YTItemType>
	{
		public static YTItemType PlayList = new(nameof(PlayList), 0);
		public static YTItemType Video = new(nameof(Video), 1);

		/// <summary>
		/// Initializes a new instance of the <see cref="YTItemType"/> class.
		/// </summary>
		/// <param name="name">Enumeration name.</param>
		/// <param name="value">Enumeration value.</param>
		private YTItemType(string name, int value) : base(name, value)
		{ }
	}
}
