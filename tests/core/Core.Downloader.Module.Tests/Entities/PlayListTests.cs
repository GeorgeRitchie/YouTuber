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
using Core.Downloader.Module.ValueObjects;

namespace Core.Downloader.Module.Tests.Entities
{
	public class PlaylistConstructorInputData
	{
		public string YouTubeId { get; set; }
		public string Url { get; set; }
		public string Title { get; set; }
		public string AuthorName { get; set; }
		public ScheduledDownload Schedule { get; set; }
		public Thumbnail? Thumbnail { get; set; }
	}

	internal class InvalidParamsForPlaylistConstructorToThrowException : TheoryData<PlaylistConstructorInputData, Type>
	{
		public InvalidParamsForPlaylistConstructorToThrowException()
		{
			var schedule = new ScheduledDownload();

			// When schedule is null
			Add(new()
			{
				YouTubeId = "example",
				Url = "https://example.com",
				Title = "Example",
				AuthorName = "Example Example",
				Schedule = null!,
				Thumbnail = null,
			}, typeof(ArgumentNullException));

			// When authorName is null
			Add(new()
			{
				YouTubeId = "example",
				Url = "https://example.com",
				Title = "Example",
				AuthorName = null!,
				Schedule = schedule,
				Thumbnail = null,
			}, typeof(ArgumentNullException));

			// When title is null
			Add(new()
			{
				YouTubeId = "example",
				Url = "https://example.com",
				Title = null!,
				AuthorName = "Example Example",
				Schedule = schedule,
				Thumbnail = null,
			}, typeof(ArgumentNullException));

			// When url is null
			Add(new()
			{
				YouTubeId = "example",
				Url = null!,
				Title = "Example",
				AuthorName = "Example Example",
				Schedule = schedule,
				Thumbnail = null,
			}, typeof(ArgumentNullException));

			// When youTubeId is null
			Add(new()
			{
				YouTubeId = null!,
				Url = "https://example.com",
				Title = "Example",
				AuthorName = "Example Example",
				Schedule = schedule,
				Thumbnail = null,
			}, typeof(ArgumentNullException));

			// When title is string.Empty
			Add(new()
			{
				YouTubeId = "example",
				Url = "https://example.com",
				Title = string.Empty,
				AuthorName = "Example Example",
				Schedule = schedule,
				Thumbnail = null,
			}, typeof(ArgumentException));

			// When url is string.Empty
			Add(new()
			{
				YouTubeId = "example",
				Url = string.Empty,
				Title = "Example",
				AuthorName = "Example Example",
				Schedule = schedule,
				Thumbnail = null,
			}, typeof(ArgumentException));

			// When youTubeId is string.Empty
			Add(new()
			{
				YouTubeId = string.Empty,
				Url = "https://example.com",
				Title = "Example",
				AuthorName = "Example Example",
				Schedule = schedule,
				Thumbnail = null,
			}, typeof(ArgumentException));

			// When title is white-space string
			Add(new()
			{
				YouTubeId = "example",
				Url = "https://example.com",
				Title = "    ",
				AuthorName = "Example Example",
				Schedule = schedule,
				Thumbnail = null,
			}, typeof(ArgumentException));

			// When url is white-space string
			Add(new()
			{
				YouTubeId = "example",
				Url = "    ",
				Title = "Example",
				AuthorName = "Example Example",
				Schedule = schedule,
				Thumbnail = null,
			}, typeof(ArgumentException));

			// When youTubeId is white-space string
			Add(new()
			{
				YouTubeId = "    ",
				Url = "https://example.com",
				Title = "Example",
				AuthorName = "Example Example",
				Schedule = schedule,
				Thumbnail = null,
			}, typeof(ArgumentException));
		}
	}

	public class PlayListTests
	{
		[Theory]
		[ClassData(typeof(InvalidParamsForPlaylistConstructorToThrowException))]
		public void Constructor_Should_ThrowException_WhenInvalidDataPassed(PlaylistConstructorInputData inputData, Type exceptionType)
		{
			// Arrange

			// Act
			var action = () => new PlayList(inputData.YouTubeId,
										   inputData.Url,
										   inputData.Title,
										   inputData.AuthorName,
										   inputData.Schedule,
										   inputData.Thumbnail);

			// Assert
			action.Should().Throw<Exception>().And.GetType().Should().Be(exceptionType);
		}

		[Fact]
		public void Constructor_Should_CreateNewInstance_WhenValidDataPassed()
		{
			// Arrange

			// Act
			var result = new PlayList("example",
										"https://example.com",
										"Example",
										"Example Example",
										new(),
										null);

			// Assert
			result.Should().NotBeNull();
		}
	}
}
