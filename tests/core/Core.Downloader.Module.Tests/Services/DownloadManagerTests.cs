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
using Core.Downloader.Module.Enumerations;
using Core.Downloader.Module.Services;
using Microsoft.Extensions.Options;
using Shared.Interfaces.Repository;

namespace Core.Downloader.Module.Tests.Services
{
	public class DownloadManagerTests
	{
		public DownloadManagerTests()
		{
			Helper.YoutubeExplodeInitializer();
		}

		[Fact]
		public void Constructor_Should_ThrowArgumentNullException_WhenNullValuePassed()
		{
			// Arrange
			var logger = new Mock<ILogger<DownloadManager>>();
			var downloader = new YouTubeDownloader(new Mock<ILogger<YouTubeDownloader>>().Object);
			var scheduledDownloadManager = new ScheduledDownloadManager(
													new Mock<IDataBase>().Object,
													new Mock<IRepository<ScheduledDownload>>().Object,
													new Mock<ILogger<ScheduledDownloadManager>>().Object);
			var currentDownloadManager = new CurrentDownloadManager(
													new Mock<ILogger<CurrentDownloadManager>>().Object,
													Options.Create(new DownloaderAppSettings()),
													downloader);

			// Act
			var action1 = () => new DownloadManager(null, currentDownloadManager, logger.Object);
			var action2 = () => new DownloadManager(scheduledDownloadManager, null, logger.Object);
			var action3 = () => new DownloadManager(scheduledDownloadManager, currentDownloadManager, null);

			// Assert
			action1.Should().ThrowExactly<ArgumentNullException>();
			action2.Should().ThrowExactly<ArgumentNullException>();
			action3.Should().ThrowExactly<ArgumentNullException>();
		}

		[Fact]
		public async Task Constructor_Should_CreateValidInstance()
		{
			// Arrange
			var logger = new Mock<ILogger<DownloadManager>>();
			var scheduledDownloadManager = new Mock<IScheduledDownloadManager>();
			var currentDownloadManager = new Mock<ICurrentDownloadManager>();
			var downloadItem = new ScheduledDownload();
			var instantDownloadItem = new ScheduledDownload() { DownloadingType = DownloadingType.Instant };
			var scheduledDownloadItem = new ScheduledDownload() { DownloadingType = DownloadingType.Scheduled };
			var id = Guid.NewGuid();

			// Act
			var downloadManager = new DownloadManager(scheduledDownloadManager.Object,
													currentDownloadManager.Object,
													logger.Object);

			_ = downloadManager.ScheduledItems;
			_ = downloadManager.DownloadingItems;
			await downloadManager.ScheduleDownloadAsync(downloadItem);
			await downloadManager.CancelScheduleAsync(id);
			await downloadManager.StartDownloadAsync(downloadItem);
			downloadManager.CancelDownload(id);

			await downloadManager.InitiateDownloadingAsync(instantDownloadItem);
			await downloadManager.InitiateDownloadingAsync(scheduledDownloadItem);

			// Assert
			downloadManager.Should().NotBeNull();
			scheduledDownloadManager.VerifyGet(i => i.ScheduledItems, Times.Once);
			currentDownloadManager.VerifyGet(i => i.DownloadingItems, Times.Once);
			scheduledDownloadManager.Verify(i => i.ScheduleDownloadAsync(It.Is<ScheduledDownload>(x => x == downloadItem),
																			It.IsAny<CancellationToken>()),
												Times.Once);
			currentDownloadManager.Verify(i => i.StartDownloadAsync(It.Is<ScheduledDownload>(x => x == downloadItem),
																			It.IsAny<Action<double>>(),
																			It.IsAny<CancellationToken>()),
												Times.Once);
			scheduledDownloadManager.Verify(i => i.ScheduleDownloadAsync(
																It.Is<ScheduledDownload>(x => x == scheduledDownloadItem),
																It.IsAny<CancellationToken>()),
												Times.Once);
			currentDownloadManager.Verify(i => i.StartDownloadAsync(
																It.Is<ScheduledDownload>(x => x == instantDownloadItem),
																It.IsAny<Action<double>>(),
																It.IsAny<CancellationToken>()),
												Times.Once);
			scheduledDownloadManager.Verify(i => i.CancelScheduleAsync(It.Is<Guid>(x => x == id),
																			It.IsAny<CancellationToken>()),
												Times.Once);
			currentDownloadManager.Verify(i => i.CancelDownload(It.Is<Guid>(x => x == id)),
												Times.Once);
		}
	}
}
