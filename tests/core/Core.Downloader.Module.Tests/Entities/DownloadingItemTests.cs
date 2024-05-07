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

namespace Core.Downloader.Module.Tests.Entities
{
	public class DownloadingItemTests
	{
		[Fact]
		public void Constructor_Should_ThrowArgumentNullException_WhenNullValuePassed()
		{
			// Arrange

			// Act
			var action = () => new DownloadingItem(null!);

			// Assert
			action.Should().ThrowExactly<ArgumentNullException>();
		}

		[Fact]
		public void Constructor_Should_CreateNewInstance_WhenAllParamsAreValid()
		{
			// Arrange
			var item = new ScheduledDownload();

			// Act
			var result = new DownloadingItem(item);

			// Assert
			result.Should().NotBeNull();
			result.Download.Should().Be(item);
		}

		[Fact]
		public void CancelDownloadingCommand_Should_CancelOperation()
		{
			// Arrange
			var item = new ScheduledDownload();
			var downloadingItem = new DownloadingItem(item);

			// Act
			downloadingItem.CancelDownloadingCommand();

			// Assert
			downloadingItem.CancellationToken.IsCancellationRequested.Should().BeTrue();
		}

		[Fact]
		public void ProgressChangedCommand_Should_TriggerProgressChangedEvent()
		{
			// Arrange
			var mockAction = new Mock<Action<double>>();
			var mockActionTwo = new Mock<Action<double>>();
			var item = new ScheduledDownload();
			var downloadingItem = new DownloadingItem(item, mockAction.Object);
			downloadingItem.ProgressChangedEvent += mockActionTwo.Object;
			double progressValue = 3.5;

			// Act
			downloadingItem.ProgressChangedCommand(progressValue);

			// Assert
			mockAction.Verify(action => action(It.Is<double>(x => x == progressValue)), Times.Once);
			mockActionTwo.Verify(action => action(It.Is<double>(x => x == progressValue)), Times.Once);
		}
	}
}
