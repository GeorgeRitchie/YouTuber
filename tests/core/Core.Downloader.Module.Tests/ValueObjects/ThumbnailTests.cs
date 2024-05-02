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

using Core.Downloader.Module.ValueObjects;

namespace Core.Downloader.Module.Tests.ValueObjects
{
	public class InvalidParamsForConstructorToThrowException : TheoryData<string, byte[], Type>
	{
		public InvalidParamsForConstructorToThrowException()
		{
			// When url is null
			Add(null!, [], typeof(ArgumentNullException));

			// When image is null
			Add("example", null!, typeof(ArgumentNullException));

			// When url is empty string
			Add(string.Empty, [], typeof(ArgumentException));

			// When url is white-space string
			Add("       ", [], typeof(ArgumentException));
		}
	}

	public sealed class ThumbnailTests
	{
		[Theory]
		[ClassData(typeof(InvalidParamsForConstructorToThrowException))]
		public void Constructor_Should_ThrowException_WhenInvalidParameterPassed(string url, byte[] image, Type exceptionType)
		{
			// Arrange

			// Act
			var action = () => new Thumbnail(url, image);

			// Assert
			action.Should().Throw<Exception>().And.GetType().Should().Be(exceptionType);
		}

		[Fact]
		public void Constructor_Should_SuccessfullyCreateNewInstance_WhenAllParamsAreValid()
		{
			// Arrange
			var url = "https://example.com";
			var image = new byte[5];

			// Act
			var thumbnail = new Thumbnail(url, image);

			// Assert
			thumbnail.Should().NotBeNull();
			thumbnail.Url.Should().Be(url);
			thumbnail.Image.Should().BeSameAs(image);
		}
	}
}
