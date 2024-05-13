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

using Core.Downloader.Module.Entities;
using Core.Downloader.Module.Enumerations;

namespace Core.Downloader.Module.Tests.Entities
{
	public class MediaStreamConstructorInputData
	{
		public double SizeInBytes { get; set; }
		public string Container { get; set; }
		public string Quality { get; set; }
		public StreamType StreamType { get; set; }
		public MediaFile MediaFile { get; set; }
	}

	internal class InvalidParamsForMediaStreamConstructorToThrowException : TheoryData<MediaStreamConstructorInputData, Type>
	{
		public InvalidParamsForMediaStreamConstructorToThrowException()
		{
			var mediaFile = new MediaFile("example",
											"https://example.com",
											"Example",
											"Example Example",
											"Example example",
											new(),
											new(),
											null);

			// When container is null
			Add(new()
			{
				SizeInBytes = 2,
				Container = null!,
				Quality = "128 Kbit/s",
				StreamType = StreamType.AudioOnly,
				MediaFile = mediaFile,
			}, typeof(ArgumentNullException));

			// When quality is null
			Add(new()
			{
				SizeInBytes = 2,
				Container = "mp3",
				Quality = null!,
				StreamType = StreamType.AudioOnly,
				MediaFile = mediaFile,
			}, typeof(ArgumentNullException));

			// When streamType is null
			Add(new()
			{
				SizeInBytes = 2,
				Container = "mp3",
				Quality = "128 Kbit/s",
				StreamType = null!,
				MediaFile = mediaFile,
			}, typeof(ArgumentNullException));

			// When mediaFile is null
			Add(new()
			{
				SizeInBytes = 2,
				Container = "mp3",
				Quality = "128 Kbit/s",
				StreamType = StreamType.AudioOnly,
				MediaFile = null!,
			}, typeof(ArgumentNullException));

			// When sizeInBytes is less then 0
			Add(new()
			{
				SizeInBytes = -2,
				Container = "mp3",
				Quality = "128 Kbit/s",
				StreamType = StreamType.AudioOnly,
				MediaFile = mediaFile,
			}, typeof(ArgumentException));

			// When container is string.Empty
			Add(new()
			{
				SizeInBytes = 2,
				Container = string.Empty,
				Quality = "128 Kbit/s",
				StreamType = StreamType.AudioOnly,
				MediaFile = mediaFile,
			}, typeof(ArgumentException));

			// When quality is string.Empty
			Add(new()
			{
				SizeInBytes = 2,
				Container = "mp3",
				Quality = string.Empty,
				StreamType = StreamType.AudioOnly,
				MediaFile = mediaFile,
			}, typeof(ArgumentException));

			// When container is white-space string
			Add(new()
			{
				SizeInBytes = 2,
				Container = "    ",
				Quality = "128 Kbit/s",
				StreamType = StreamType.AudioOnly,
				MediaFile = mediaFile,
			}, typeof(ArgumentException));

			// When quality is white-space string
			Add(new()
			{
				SizeInBytes = 2,
				Container = "mp3",
				Quality = "    ",
				StreamType = StreamType.AudioOnly,
				MediaFile = mediaFile,
			}, typeof(ArgumentException));
		}
	}

	public class MediaStreamTests
	{
		[Theory]
		[ClassData(typeof(InvalidParamsForMediaStreamConstructorToThrowException))]
		public void Constructor_Should_ThrowException_WhenInvalidDataPassed(MediaStreamConstructorInputData inputData, Type exceptionType)
		{
			// Arrange

			// Act
			var action = () => new MediaStream(inputData.SizeInBytes,
												inputData.Container,
												inputData.Quality,
												inputData.StreamType,
												inputData.MediaFile);

			// Assert
			action.Should().Throw<Exception>().And.GetType().Should().Be(exceptionType);
		}

		[Fact]
		public void Constructor_Should_CreateNewInstance_WhenValidDataPassed()
		{
			// Arrange
			var mediaFile = new MediaFile("example",
											"https://example.com",
											"Example",
											"Example Example",
											"Example example",
											new(),
											new(),
											null);

			// Act
			var result = new MediaStream(2,
										"mp3",
										"128 Kbit/s",
										StreamType.AudioOnly,
										mediaFile);

			// Assert
			result.Should().NotBeNull();
			result.MediaFileId.Should().Be(mediaFile.Id);
		}
	}
}
