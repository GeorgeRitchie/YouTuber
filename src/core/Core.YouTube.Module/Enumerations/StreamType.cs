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
	/// Represents YouTube media stream types enumeration.
	/// </summary>
	public sealed class StreamType : Enumeration<StreamType>
	{
		public static StreamType VideoOnly = new(nameof(VideoOnly), 0);
		public static StreamType AudioOnly = new(nameof(AudioOnly), 1);
		public static StreamType Mixed = new(nameof(Mixed), 2);

		/// <summary>
		/// Initializes a new instance of the <see cref="StreamType"/> class.
		/// </summary>
		/// <param name="name">Enumeration name.</param>
		/// <param name="value">Enumeration value.</param>
		private StreamType(string name, int value) : base(name, value)
		{ }
	}
}
