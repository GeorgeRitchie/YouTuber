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

using Core.YouTube.Module.Enumerations;

namespace Core.YouTube.Module.Entities
{
	/// <summary>
	/// Represents YouTube media stream.
	/// </summary>
	public sealed class MediaStream
	{
		/// <summary>
		/// Media stream size in bytes.
		/// </summary>
		public long SizeInBytes { get; init; }

		/// <summary>
		/// Media stream container name (e.g. mp4, webm, mp3, mp4a, etc). Can be used as file extension.
		/// </summary>
		public string Container { get; init; }

		/// <summary>
		/// Media stream quality label (e.g. 1080p, 720p60, 128 Kbit/s, etc).
		/// </summary>
		public string Quality { get; init; }

		/// <summary>
		/// Media stream type.
		/// </summary>
		public StreamType Type { get; init; }
	}
}
