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

using Core.YouTube.Module.Entities;
using Core.YouTube.Module.Services;
using Microsoft.Extensions.Logging;
using YoutubeExplode;

namespace Core.YouTube.Module.Tests.Services
{
	internal class LoggerStub<T> : ILogger<T>, ILogger
	{
		public IDisposable? BeginScope<TState>(TState state) where TState : notnull
		{
			return null;
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return false;
		}

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
		{

		}
	}

	public class InvalidParamsForYouTubeServiceToGetItemInfoToThrowException : TheoryData<string, Type>
	{
		public InvalidParamsForYouTubeServiceToGetItemInfoToThrowException()
		{
			// When url is null
			Add(null!, typeof(ArgumentNullException));

			// When url is empty string
			Add(string.Empty, typeof(ArgumentException));

			// When url is white-space string
			Add("       ", typeof(ArgumentException));
		}
	}

	internal class ActionsToTestOperationCancelationException : TheoryData<Func<IYouTubeService, Task>>
	{
		public ActionsToTestOperationCancelationException()
		{
			// Test for GetItemInfoAsync method
			Add(async (youtubeService) =>
			{
				var tokenSource = new CancellationTokenSource();
				await youtubeService.GetItemInfoAsync("https://youtube/example/url", tokenSource.Token);
				tokenSource.Cancel();
			});

			// Test for GetVideoInfoAsync method
			Add(async (youtubeService) =>
			{
				var tokenSource = new CancellationTokenSource();
				await youtubeService.GetVideoInfoAsync("https://youtube/example/url", tokenSource.Token);
				tokenSource.Cancel();
			});

			// Test for GetPlayListInfoAsync method
			Add(async (youtubeService) =>
			{
				var tokenSource = new CancellationTokenSource();
				await youtubeService.GetPlayListInfoAsync("https://youtube/example/url", tokenSource.Token);
				tokenSource.Cancel();
			});
		}
	}

	public class YouTubeServiceTests
	{
		public YouTubeServiceTests()
		{
			Helper.YoutubeExplodeInitializer();
		}

		[Fact]
		public void Constructor_Should_ThrowException_WhenNullValuePassed()
		{
			// Arrange

			// Act
			var actionLoggerNull = () => new YouTubeService(null!, new());
			var actionYouTubeNull = () => new YouTubeService(new LoggerStub<YouTubeService>(), null!);

			// Assert
			actionLoggerNull.Should().Throw<ArgumentNullException>();
			actionYouTubeNull.Should().Throw<ArgumentNullException>();
		}

		[Theory]
		[ClassData(typeof(InvalidParamsForYouTubeServiceToGetItemInfoToThrowException))]
		public void GetItemInfoAsync_Should_ThrowException_WhenInvalidDataPassed(string url, Type exceptionType)
		{
			// Arrange
			var youtubeService = new YouTubeService(new LoggerStub<YouTubeService>(), new());

			// Act
			var action = () => youtubeService.GetItemInfoAsync(url).Result;

			// Assert
			action.Should().Throw<Exception>().And.GetType().Should().Be(exceptionType);
		}

		[Theory]
		[ClassData(typeof(InvalidParamsForYouTubeServiceToGetItemInfoToThrowException))]
		public void GetVideoInfoAsync_Should_ThrowException_WhenInvalidDataPassed(string url, Type exceptionType)
		{
			// Arrange
			var youtubeService = new YouTubeService(new LoggerStub<YouTubeService>(), new());

			// Act
			var action = () => youtubeService.GetVideoInfoAsync(url).Result;

			// Assert
			action.Should().Throw<Exception>().And.GetType().Should().Be(exceptionType);
		}

		[Theory]
		[ClassData(typeof(InvalidParamsForYouTubeServiceToGetItemInfoToThrowException))]
		public void GetPlayListInfoAsync_Should_ThrowException_WhenInvalidDataPassed(string url, Type exceptionType)
		{
			// Arrange
			var youtubeService = new YouTubeService(new LoggerStub<YouTubeService>(), new());

			// Act
			var action = () => youtubeService.GetPlayListInfoAsync(url).Result;

			// Assert
			action.Should().Throw<Exception>().And.GetType().Should().Be(exceptionType);
		}

		[Theory]
		[ClassData(typeof(ActionsToTestOperationCancelationException))]
		public void AnyMethodWithCancelationParam_Should_ThrowOperationCanceledException_WhenCancelationRequested(Func<IYouTubeService, Task> action)
		{
			// Arrange
			var youtubeService = new YouTubeService(new LoggerStub<YouTubeService>(), new());

			// Act
			var testAction = async () => await action(youtubeService);

			// Assert
			testAction.Should().ThrowExactlyAsync<OperationCanceledException>();
		}

		[Fact]
		public async Task AnyAnalyzerMethods_Should_ReturnFailureResult_WhenInvalidUrlPassed()
		{
			// Arrange
			// We cannot create Mock object for YoutubeClient client, since specific implementation of YoutubeClient.
			var youtubeClient = new YoutubeClient();
			var youtubeService = new YouTubeService(new LoggerStub<YouTubeService>(), youtubeClient);
			var testUrl = "http://testurl.com";

			// Act
			var result1 = await youtubeService.GetItemInfoAsync(testUrl);
			var result2 = await youtubeService.GetPlayListInfoAsync(testUrl);
			var result3 = await youtubeService.GetVideoInfoAsync(testUrl);

			// Assert
			result1.Should().NotBeNull();
			result1.Value.Should().BeNull();
			result1.IsSuccess.Should().BeFalse();
			result2.Should().NotBeNull();
			result2.Value.Should().BeNull();
			result2.IsSuccess.Should().BeFalse();
			result3.Should().NotBeNull();
			result3.Value.Should().BeNull();
			result3.IsSuccess.Should().BeFalse();
		}

		[Fact]
		public async Task GetItemInfoAsync_Should_ReturnSuccessResult_WhenValidUrlPassed()
		{
			// Arrange
			// We cannot create Mock object for YoutubeClient client, since specific implementation of YoutubeClient.
			var youtubeService = new YouTubeService(new LoggerStub<YouTubeService>(), new());
			var playlistUrl = "https://www.youtube.com/playlist?list=PLYpjLpq5ZDGu0-hCBCw9d_t0j-oOn3RYr";
			var videoUrl = "https://www.youtube.com/watch?v=McDvyFglkvU&list=PLYpjLpq5ZDGu0-hCBCw9d_t0j-oOn3RYr";

			// Act
			var playlistResult = await youtubeService.GetItemInfoAsync(playlistUrl);
			var videoResult = await youtubeService.GetItemInfoAsync(videoUrl);

			// Assert
			playlistResult.Should().NotBeNull();
			playlistResult.IsSuccess.Should().BeTrue();
			playlistResult.Value.Should().NotBeNull().And.BeAssignableTo<PlayList>();
			playlistResult.Value!.Thumbnail.Should().NotBeNull();
			playlistResult.Value!.Thumbnail!.Image.Should().NotBeNullOrEmpty();
			(playlistResult!.Value as PlayList)!.Videos.Should().NotBeNullOrEmpty();

			videoResult.Should().NotBeNull();
			videoResult.IsSuccess.Should().BeTrue();
			videoResult.Value.Should().NotBeNull().And.BeAssignableTo<Video>();
			videoResult.Value!.Thumbnail.Should().NotBeNull();
			videoResult.Value!.Thumbnail!.Image.Should().NotBeNullOrEmpty();
			(videoResult!.Value as Video)!.Streams.Should().NotBeNullOrEmpty();
		}

		[Fact]
		public async Task GetPlayListInfoAsync_Should_ReturnSuccessResult_WhenValidUrlPassed()
		{
			// Arrange
			// We cannot create Mock object for YoutubeClient client, since specific implementation of YoutubeClient.
			var youtubeService = new YouTubeService(new LoggerStub<YouTubeService>(), new());
			var playlistUrl = "https://www.youtube.com/playlist?list=PLYpjLpq5ZDGu0-hCBCw9d_t0j-oOn3RYr";

			// Act
			var playlistResult = await youtubeService.GetPlayListInfoAsync(playlistUrl);

			// Assert
			playlistResult.Should().NotBeNull();
			playlistResult.IsSuccess.Should().BeTrue();
			playlistResult.Value.Should().NotBeNull().And.BeAssignableTo<PlayList>();
			playlistResult.Value!.Thumbnail.Should().NotBeNull();
			playlistResult.Value!.Thumbnail!.Image.Should().NotBeNullOrEmpty();
			playlistResult!.Value!.Videos.Should().NotBeNullOrEmpty();
		}

		[Fact]
		public async Task GetVideoInfoAsync_Should_ReturnSuccessResult_WhenValidUrlPassed()
		{
			// Arrange
			// We cannot create Mock object for YoutubeClient client, since specific implementation of YoutubeClient.
			var youtubeService = new YouTubeService(new LoggerStub<YouTubeService>(), new());
			var videoUrl = "https://www.youtube.com/watch?v=McDvyFglkvU&list=PLYpjLpq5ZDGu0-hCBCw9d_t0j-oOn3RYr";

			// Act
			var videoResult = await youtubeService.GetVideoInfoAsync(videoUrl);

			// Assert
			videoResult.Should().NotBeNull();
			videoResult.IsSuccess.Should().BeTrue();
			videoResult.Value.Should().NotBeNull().And.BeAssignableTo<Video>();
			videoResult.Value!.Thumbnail.Should().NotBeNull();
			videoResult.Value!.Thumbnail!.Image.Should().NotBeNullOrEmpty();
			videoResult!.Value!.Streams.Should().NotBeNullOrEmpty();
		}
	}
}
