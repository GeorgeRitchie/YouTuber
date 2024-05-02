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

namespace Core.Downloader.Module.Tests.Entities
{
	public class TimingTests
	{
		[Fact]
		public void CreateForFixedDateTime_Should_CreateValidInstance()
		{
			// Arrange
			var startDateTime = DateTime.UtcNow;
			var endDateTime = DateTime.UtcNow.AddDays(3).AddHours(2);

			// Act
			var result = Timing.CreateForFixedDateTime(startDateTime, endDateTime);

			// Assert
			result.Type.Should().Be(TimingType.FixedDateTime);
			result.StartDate.Should().Be(DateOnly.FromDateTime(startDateTime));
			result.EndDate.Should().Be(DateOnly.FromDateTime(endDateTime));
			result.StartTime.Should().Be(TimeOnly.FromDateTime(startDateTime));
			result.EndTime.Should().Be(TimeOnly.FromDateTime(endDateTime));
		}

		[Fact]
		public void CreateForDateFixedOnly_Should_CreateValidInstance()
		{
			// Arrange
			var startDateOnly = DateOnly.FromDateTime(DateTime.UtcNow);
			var endDateOnly = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(3));

			// Act
			var result = Timing.CreateForDateFixedOnly(startDateOnly, endDateOnly);

			// Assert
			result.Type.Should().Be(TimingType.DateFixedOnly);
			result.StartDate.Should().Be(startDateOnly);
			result.EndDate.Should().Be(endDateOnly);
			result.StartTime.Should().BeNull();
			result.EndTime.Should().BeNull();
		}

		[Fact]
		public void CreateForTimeFixedOnly_Should_CreateValidInstance()
		{
			// Arrange
			var startTimeOnly = TimeOnly.FromDateTime(DateTime.UtcNow);
			var endTimeOnly = TimeOnly.FromDateTime(DateTime.UtcNow.AddDays(3));

			// Act
			var result = Timing.CreateForTimeFixedOnly(startTimeOnly, endTimeOnly);

			// Assert
			result.Type.Should().Be(TimingType.TimeFixedOnly);
			result.StartDate.Should().BeNull();
			result.EndDate.Should().BeNull();
			result.StartTime.Should().Be(startTimeOnly);
			result.EndTime.Should().Be(endTimeOnly);
		}

		[Fact]
		public void CreateForDateFixedTimeInterval_Should_CreateValidInstance()
		{
			// Arrange
			var startDateOnly = DateOnly.FromDateTime(DateTime.UtcNow);
			var endDateOnly = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(3));
			var startTimeOnly = TimeOnly.FromDateTime(DateTime.UtcNow);
			var endTimeOnly = TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(2).AddMinutes(24));

			// Act
			var result = Timing.CreateForDateFixedTimeInterval(startDateOnly, endDateOnly, startTimeOnly, endTimeOnly);

			// Assert
			result.Type.Should().Be(TimingType.DateFixedTimeInterval);
			result.StartDate.Should().Be(startDateOnly);
			result.EndDate.Should().Be(endDateOnly);
			result.StartTime.Should().Be(startTimeOnly);
			result.EndTime.Should().Be(endTimeOnly);
		}
	}
}
