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
	internal class PropertySetActionShouldThrowArgumentNullException : TheoryData<Func<object>>
	{
		public PropertySetActionShouldThrowArgumentNullException()
		{
			var scheduledDownload = new ScheduledDownload();

			// DownloadingType property null set action
			Add(() => scheduledDownload.DownloadingType = null!);

			// Timing property null set action
			Add(() => scheduledDownload.Timing = null!);

			// MediaFile property null set action
			Add(() => scheduledDownload.MediaFile = null!);
		}
	}

	public class ScheduledDownloadTests
	{
		[Theory]
		[ClassData(typeof(PropertySetActionShouldThrowArgumentNullException))]
		public void DownloadingTypePropertySet_Should_ThrowArgumentNullException_WhenNullAssigned(Func<object> action)
		{
			// Arrange

			// Act

			// Assert
			action.Should().ThrowExactly<ArgumentNullException>();
		}
	}
}
