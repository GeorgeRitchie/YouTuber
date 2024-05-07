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
using Core.Downloader.Module.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Interfaces.Repository;

namespace Core.Downloader.Module.Tests.Services
{
	public class ScheduledDownloadManagerTests
	{
		[Fact]
		public void Constructor_Should_ThrowArgumentNullException_WhenNullPassed()
		{
			// Arrange

			// Act
			var action1 = () => new ScheduledDownloadManager(null,
														new Mock<IRepository<ScheduledDownload>>().Object,
														new Mock<ILogger<ScheduledDownloadManager>>().Object);
			var action2 = () => new ScheduledDownloadManager(new Mock<IDataBase>().Object,
														null,
														new Mock<ILogger<ScheduledDownloadManager>>().Object);
			var action3 = () => new ScheduledDownloadManager(null,
														new Mock<IRepository<ScheduledDownload>>().Object,
														null);

			// Assert
			action1.Should().ThrowExactly<ArgumentNullException>();
			action2.Should().ThrowExactly<ArgumentNullException>();
			action3.Should().ThrowExactly<ArgumentNullException>();
		}

		[Fact]
		public void ScheduledItems_Should_GetDataFromDb()
		{
			// Arrange
			var repository = new Mock<IRepository<ScheduledDownload>>();
			var manager = new ScheduledDownloadManager(new Mock<IDataBase>().Object,
														repository.Object,
														new Mock<ILogger<ScheduledDownloadManager>>().Object);
			repository.Setup(i => i.GetAllAsNoTracking()).Returns(new List<ScheduledDownload>().AsQueryable());

			// Act
			_ = manager.ScheduledItems;

			// Assert
			repository.Verify(i => i.GetAllAsNoTracking(), Times.Once);
		}

		[Fact]
		public async Task ScheduleDownloadAsync_Should_AddNewItemToDb()
		{
			// Arrange
			var item = new ScheduledDownload();
			var db = new Mock<IDataBase>();
			db.Setup(i => i.BeginTransactionAsync(It.IsAny<CancellationToken>()))
											.Returns(Task.FromResult(new Mock<IDbContextTransaction>().Object));
			var repository = new Mock<IRepository<ScheduledDownload>>();
			var manager = new ScheduledDownloadManager(db.Object,
														repository.Object,
														new Mock<ILogger<ScheduledDownloadManager>>().Object);

			// Act
			await manager.ScheduleDownloadAsync(item);

			// Assert
			repository.Verify(i => i.Create(It.Is<ScheduledDownload>(x => x == item)), Times.Once);
			db.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task CancelScheduleAsync_Should_RemoveItemFromDb()
		{
			// Arrange
			var item = new ScheduledDownload();
			var db = new Mock<IDataBase>();
			db.Setup(i => i.BeginTransactionAsync(It.IsAny<CancellationToken>()))
											.Returns(Task.FromResult(new Mock<IDbContextTransaction>().Object));
			var repository = new Mock<IRepository<ScheduledDownload>>();
			var manager = new ScheduledDownloadManager(db.Object,
														repository.Object,
														new Mock<ILogger<ScheduledDownloadManager>>().Object);
			var id = Guid.NewGuid();

			// Act
			await manager.CancelScheduleAsync(id);

			// Assert
			repository.Verify(i => i.Delete(It.Is<Guid>(x => x == id)), Times.Once);
			db.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}
	}
}
