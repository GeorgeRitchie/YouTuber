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

using Core.Downloader.Module.Configurations;
using Core.Downloader.Module.Entities;
using Core.Downloader.Module.Services;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Core.Downloader.Module.Tests.Services
{
	public class CurrentDownloadManagerTests
	{
		[Fact]
		public void Constructor_Should_ThrowArgumentNullException_WhenNullValuePassed()
		{
			// Arrange
			var option = Options.Create(new DownloaderAppSettings());
			var logger = new Mock<ILogger<CurrentDownloadManager>>();
			var downloader = new Mock<IDownloader>();

			// Act
			var action1 = () => new CurrentDownloadManager(null, option, downloader.Object);
			var action2 = () => new CurrentDownloadManager(logger.Object, null, downloader.Object);
			var action3 = () => new CurrentDownloadManager(logger.Object, option, null);

			// Assert
			action1.Should().ThrowExactly<ArgumentNullException>();
			action2.Should().ThrowExactly<ArgumentNullException>();
			action3.Should().ThrowExactly<ArgumentNullException>();
		}

		[Fact]
		public async Task DownloadingItems_Should_ReturnCurrentlyDownloadingItems()
		{
			// Arrange
			var option = Options.Create(new DownloaderAppSettings()
			{
				DownloadedFilesDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
			});
			var manager = new CurrentDownloadManager(new Mock<ILogger<CurrentDownloadManager>>().Object,
													 option,
													 new Mock<IDownloader>().Object);
			var downloadItem = new ScheduledDownload
			{
				MediaFile = Initiator.CreateNewMediaFile()
			};
			downloadItem.MediaFile.Stream = Initiator.CreateNewAudioOnlyMediaStream();

			// Act
			await manager.StartDownloadAsync(downloadItem);
			var result = manager.DownloadingItems;

			// Assert
			result.Should().NotBeNull().And.Contain(x => x.Download == downloadItem);
		}

		[Fact]
		public async Task StartDownloadAsync_Should_StartNewDownloading()
		{
			// Arrange
			var option = Options.Create(new DownloaderAppSettings()
			{
				DownloadedFilesDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
			});
			var downloader = new Mock<IDownloader>();
			var manager = new CurrentDownloadManager(new Mock<ILogger<CurrentDownloadManager>>().Object,
													 option,
													 downloader.Object);
			var downloadItem = new ScheduledDownload
			{
				MediaFile = Initiator.CreateNewMediaFile()
			};
			downloadItem.MediaFile.Stream = Initiator.CreateNewAudioOnlyMediaStream();

			// Act
			await manager.StartDownloadAsync(downloadItem);
			var result = manager.DownloadingItems;

			// Assert
			result.Should().NotBeNull().And.Contain(x => x.Download == downloadItem);
			downloader.Verify(i => i.DownloadMediaAsync(It.Is<Guid>(x => x == downloadItem.Id),
														It.Is<MediaStream>(x => x == downloadItem.MediaFile.Stream),
														It.Is<string>(x => x == downloadItem.MediaFile.YouTubeId),
														It.IsAny<string>(),
														It.IsAny<IProgress<double>>(),
														It.IsAny<CancellationToken>()),
								Times.Once);

		}

		[Fact]
		public async Task CancelDownload_Should_StartNewDownloading()
		{
			// Arrange
			var option = Options.Create(new DownloaderAppSettings()
			{
				DownloadedFilesDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
			});
			var downloader = new Mock<IDownloader>();
			var manager = new CurrentDownloadManager(new Mock<ILogger<CurrentDownloadManager>>().Object,
													 option,
													 downloader.Object);
			var downloadItem = new ScheduledDownload
			{
				MediaFile = Initiator.CreateNewMediaFile()
			};
			downloadItem.MediaFile.Stream = Initiator.CreateNewAudioOnlyMediaStream();

			// Act
			await manager.StartDownloadAsync(downloadItem);
			DownloadingItem downloadingRecord = (DownloadingItem)manager.DownloadingItems[0];
			manager.CancelDownload(downloadingRecord.Download.Id);
			var result = manager.DownloadingItems;

			// Assert
			result.Should().BeEmpty();
			downloadingRecord.CancellationToken.IsCancellationRequested.Should().BeTrue();
		}
	}
}
