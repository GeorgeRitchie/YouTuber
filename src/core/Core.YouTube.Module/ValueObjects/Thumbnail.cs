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

namespace Core.YouTube.Module.ValueObjects
{
	/// <summary>
	/// Represents a thumbnail for YouTube item.
	/// </summary>
	public sealed class Thumbnail : ValueObject
	{
		/// <summary>
		/// Url for original source, from where thumbnail was downloaded.
		/// </summary>
		public string Url { get; init; }

		/// <summary>
		/// Thumbnail as <see langword="byte[]"/> image.
		/// </summary>
		public byte[] Image { get; init; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Thumbnail"/> class.
		/// </summary>
		/// <param name="url">Thumbnail url.</param>
		/// <param name="image">Thumbnail as <see langword="byte[]"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when any param is null.</exception>
		/// <exception cref="ArgumentException">Thrown when url is <see cref="string.Empty"/> or white-space string.</exception>
		public Thumbnail(string url, byte[] image)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(url, nameof(url));
			ArgumentNullException.ThrowIfNull(image, nameof(image));

			Url = url;
			Image = image;
		}

		/// <inheritdoc/>
		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Url;
			yield return Image;
		}
	}
}
