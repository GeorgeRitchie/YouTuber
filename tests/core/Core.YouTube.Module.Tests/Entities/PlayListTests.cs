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

namespace Core.YouTube.Module.Tests.Entities
{
	public class PlayListTests
	{
		[Fact]
		public void Constructor_Should_CreateNewInstanceWithEmptyCollectionOfVideos_WhenNoValuesAssigned()
		{
			// Arrange

			// Act
			var playList = new PlayList();

			// Assert
			playList.Videos.Should().NotBeNull();
			playList.Videos.Should().BeEmpty();
		}

		[Fact]
		public void Constructor_Should_ThrowArgumentNullException_WhenNullPassedForVideosCollection()
		{
			// Arrange

			// Act
			var action = () => new PlayList() { Videos = null! };

			// Assert
			action.Should().ThrowExactly<ArgumentNullException>();
		}
	}
}
