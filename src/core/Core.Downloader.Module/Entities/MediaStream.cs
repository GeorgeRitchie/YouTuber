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

using Core.Downloader.Module.Enumerations;

namespace Core.Downloader.Module.Entities
{
	/// <summary>
	/// Represents a media file's media stream metadata.
	/// </summary>
	public sealed class MediaStream
	{
		/// <summary>
		/// Gets the unique identifier of the media stream.
		/// </summary>
		public Guid Id { get; private init; }

		/// <summary>
		/// Gets the size of the media stream in bytes.
		/// </summary>
		public double SizeInBytes { get; private init; }

		/// <summary>
		/// Gets the container format of the media stream (e.g. mp4, webm, mp3, mp4a, etc). Can be used as file extension.
		/// </summary>
		public string Container { get; private init; }

		/// <summary>
		/// Gets the quality description of the media stream (e.g. 1080p, 720p60, 128 Kbit/s, etc).
		/// </summary>
		public string Quality { get; private init; }

		/// <summary>
		/// Gets the type of the media stream.
		/// </summary>
		public StreamType StreamType { get; private init; }

		/// <summary>
		/// Gets the identifier of the associated media file.
		/// </summary>
		public Guid MediaFileId { get; private init; }

		/// <summary>
		/// Gets the associated media file.
		/// </summary>
		public MediaFile MediaFile { get; private init; }

		/// <summary>
		/// Initializes a new instance of the <see cref="MediaStream"/> class.
		/// </summary>
		/// <remarks>Used only by EF Core.</remarks>
		private MediaStream()
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="MediaStream"/> class.
		/// </summary>
		/// <param name="sizeInBytes">The size of the media stream in bytes.</param>
		/// <param name="container">The container format of the media stream.</param>
		/// <param name="quality">The quality description of the media stream.</param>
		/// <param name="streamType">The type of the media stream.</param>
		/// <param name="mediaFile">The media file associated with this stream.</param>
		/// <exception cref="ArgumentNullException">Thrown if any parameter, except <paramref name="sizeInBytes"/>, is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">
		/// Thrown if <paramref name="container"/> or <paramref name="quality"/> is <see cref="string.Empty"/> or white-space string,
		/// or if <paramref name="sizeInBytes"/> is negative.
		/// </exception>
		public MediaStream(double sizeInBytes, string container, string quality, StreamType streamType, MediaFile mediaFile)
		{
			ArgumentNullException.ThrowIfNull(streamType, nameof(streamType));
			ArgumentNullException.ThrowIfNull(mediaFile, nameof(mediaFile));

			ArgumentException.ThrowIfNullOrWhiteSpace(container, nameof(container));
			ArgumentException.ThrowIfNullOrWhiteSpace(quality, nameof(quality));

			if (sizeInBytes < 0)
				throw new ArgumentException(null, nameof(sizeInBytes));

			SizeInBytes = sizeInBytes;
			Container = container;
			Quality = quality;
			StreamType = streamType;
			MediaFile = mediaFile;
			MediaFileId = mediaFile.Id;

			Id = Guid.NewGuid();
		}
	}
}
