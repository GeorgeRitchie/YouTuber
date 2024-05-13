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
using Core.Downloader.Module.Services;
using System.Reflection;

namespace Core.Downloader.Module.Tests.Services
{
	internal class InvalidDataForDownloadMediaAsyncToThrowException : TheoryData<Guid, MediaStream, string, string, Type>
	{
		public InvalidDataForDownloadMediaAsyncToThrowException()
		{
			var stream = Initiator.CreateNewAudioOnlyMediaStream();

			// When stream is null
			Add(Guid.NewGuid(), null!, "Example", "Example", typeof(ArgumentNullException));

			// When mediaId is null
			Add(Guid.NewGuid(), stream, null!, "Example", typeof(ArgumentNullException));

			// When outputPath is null
			Add(Guid.NewGuid(), stream, "Example", null!, typeof(ArgumentNullException));

			// When mediaId is string.Empty
			Add(Guid.NewGuid(), stream, string.Empty, "Example", typeof(ArgumentException));

			// When outputPath is string.Empty
			Add(Guid.NewGuid(), stream, "Example", string.Empty, typeof(ArgumentException));

			// When mediaId is white-space string
			Add(Guid.NewGuid(), stream, "    ", "Example", typeof(ArgumentException));

			// When outputPath is white-space string
			Add(Guid.NewGuid(), stream, "Example", "    ", typeof(ArgumentException));

			// When stream has new not implemented StreamType
			ConstructorInfo constructor = typeof(StreamType).GetConstructor(
				BindingFlags.Instance | BindingFlags.NonPublic,
				null,
				new[] { typeof(string), typeof(int) },
				null)!;
			StreamType unknownStreamType = constructor.Invoke(new object[] { "CustomName", 9999 }) as StreamType;
			var unknownStream = new MediaStream(2, "mp3", "128 Kbit/s", unknownStreamType, Initiator.CreateNewMediaFile());
			Add(Guid.NewGuid(), unknownStream, "https://www.youtube.com/shorts/-I9NVTob45k", "Example", typeof(NotImplementedException));
		}
	}

	public class YouTubeDownloaderTests
	{
		public YouTubeDownloaderTests()
		{
			Helper.YoutubeExplodeInitializer();
		}

		[Fact]
		public void Constructor_Should_ThrowArgumentNullException_WhenNullPassedToParams()
		{
			// Arrange

			// Act
			var action = () => new YouTubeDownloader(null!);

			// Assert
			action.Should().ThrowExactly<ArgumentNullException>();
		}

		[Theory]
		[ClassData(typeof(InvalidDataForDownloadMediaAsyncToThrowException))]
		public void DownloadMediaAsync_Should_ThrowException_WhenInvalidDataPassed(Guid requestId,
																					MediaStream stream,
																					string mediaId,
																					string outputPath,
																					Type exceptionType)
		{
			// Arrange
			var logger = Initiator.CreateConsoleLogger<YouTubeDownloader>();
			var downloader = new YouTubeDownloader(logger);

			// Act
			var action = () => downloader.DownloadMediaAsync(requestId, stream, mediaId, outputPath).Wait();

			// Assert
			action.Should().Throw<Exception>().And.GetType().Should().Be(exceptionType);
		}
	}
}
