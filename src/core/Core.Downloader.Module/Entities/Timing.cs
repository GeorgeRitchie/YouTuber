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

using Core.Downloader.Module.Enumerations;

namespace Core.Downloader.Module.Entities
{
	/// <summary>
	/// Represents scheduler timing entity.
	/// </summary>
	public sealed class Timing
	{
		/// <summary>
		/// Gets the timing entity identifier.
		/// </summary>
		public Guid Id { get; private init; } = Guid.NewGuid();

		/// <summary>
		/// Gets the timing entity type.
		/// </summary>
		public TimingType Type { get; private init; }

		/// <summary>
		/// Gets start date of scheduling period if available, otherwise <see langword="null"/>.
		/// </summary>
		public DateOnly? StartDate { get; private init; }

		/// <summary>
		/// Gets end date of scheduling period if available, otherwise <see langword="null"/>.
		/// </summary>
		public DateOnly? EndDate { get; private init; }

		/// <summary>
		/// Gets start time of scheduling period if available, otherwise <see langword="null"/>.
		/// </summary>
		public TimeOnly? StartTime { get; private init; }

		/// <summary>
		/// Gets end time of scheduling period if available, otherwise <see langword="null"/>.
		/// </summary>
		public TimeOnly? EndTime { get; private init; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Timing"/> class.
		/// </summary>
		private Timing()
		{ }

		/// <summary>
		/// Creates a new instance of <see cref="Timing"/> with the type <see cref="TimingType.FixedDateTime"/>.
		/// This timing configuration is used for scheduling that starts and ends at specific dates and times.
		/// </summary>
		/// <param name="start">The start date and time of the scheduling period.</param>
		/// <param name="end">The end date and time of the scheduling period.</param>
		/// <returns>A <see cref="Timing"/> instance configured for a fixed date and time range, 
		/// where actions can be applied strictly between the specified start and end times.</returns>
		/// <example>
		/// The following code demonstrates how to create a <see cref="Timing"/> object for a scheduling period
		/// that starts on May 1, 2024, at 11:04:35 AM and ends on May 3, 2024, at 09:35:23 AM:
		/// <code>
		/// DateTime start = new DateTime(2024, 5, 1, 11, 4, 35);
		/// DateTime end = new DateTime(2024, 5, 3, 9, 35, 23);
		/// Timing timing = Timing.CreateForFixedDateTime(start, end);
		/// </code>
		/// </example>
		public static Timing CreateForFixedDateTime(DateTime start, DateTime end)
		{
			return new()
			{
				Type = TimingType.FixedDateTime,
				StartDate = DateOnly.FromDateTime(start),
				EndDate = DateOnly.FromDateTime(end),
				StartTime = TimeOnly.FromDateTime(start),
				EndTime = TimeOnly.FromDateTime(end),
			};
		}

		/// <summary>
		/// Creates a new instance of <see cref="Timing"/> with the type <see cref="TimingType.DateFixedOnly"/>.
		/// This timing configuration specifies only the start and end dates, treating the start time as 00:00:00 and 
		/// the end time as 23:59:59 on the respective dates.
		/// </summary>
		/// <param name="startDate">The start date of the scheduling period, time considered as 00:00:00.</param>
		/// <param name="endDate">The end date of the scheduling period, time considered as 23:59:59.</param>
		/// <returns>A <see cref="Timing"/> instance configured for a date-only range, implying that the action 
		/// is applicable from the beginning of the start date to the end of the end date.</returns>
		/// <example>
		/// The following code demonstrates how to create a <see cref="Timing"/> object for a scheduling period
		/// that applies from the start of May 1, 2024, to the end of May 3, 2024:
		/// <code>
		/// DateOnly startDate = new DateOnly(2024, 5, 1);
		/// DateOnly endDate = new DateOnly(2024, 5, 3);
		/// Timing timing = CreateForDateFixedOnly(startDate, endDate);
		/// </code>
		/// </example>
		public static Timing CreateForDateFixedOnly(DateOnly startDate, DateOnly endDate)
		{
			return new()
			{
				Type = TimingType.DateFixedOnly,
				StartDate = startDate,
				EndDate = endDate,
				StartTime = null,
				EndTime = null,
			};
		}

		/// <summary>
		/// Creates a new instance of <see cref="Timing"/> with the type <see cref="TimingType.TimeFixedOnly"/>.
		/// This timing configuration specifies only the start and end times, implying that the action is applicable 
		/// every day within these times, regardless of the date.
		/// </summary>
		/// <param name="startTime">The start time of the scheduling period each day.</param>
		/// <param name="endTime">The end time of the scheduling period each day.</param>
		/// <returns>A <see cref="Timing"/> instance configured for a time-only range, meaning that the action 
		/// can be applied daily between the specified start and end times.</returns>
		/// <example>
		/// The following code demonstrates how to create a <see cref="Timing"/> object for a scheduling period
		/// that applies every day from 10:17:09 AM to 2:58:41 PM:
		/// <code>
		/// TimeOnly startTime = new TimeOnly(10, 17, 9);
		/// TimeOnly endTime = new TimeOnly(14, 58, 41);
		/// Timing timing = CreateForTimeFixedOnly(startTime, endTime);
		/// </code>
		/// </example>
		public static Timing CreateForTimeFixedOnly(TimeOnly startTime, TimeOnly endTime)
		{
			return new()
			{
				Type = TimingType.TimeFixedOnly,
				StartDate = null,
				EndDate = null,
				StartTime = startTime,
				EndTime = endTime,
			};
		}

		/// <summary>
		/// Creates a new instance of <see cref="Timing"/> with the type <see cref="TimingType.DateFixedTimeInterval"/>.
		/// This timing configuration specifies both date and time intervals, allowing an action to be applied 
		/// every day within a specified date range, during specified start and end times each day.
		/// </summary>
		/// <param name="startDate">The start date of the scheduling period.</param>
		/// <param name="endDate">The end date of the scheduling period.</param>
		/// <param name="startTime">The start time each day within the date range.</param>
		/// <param name="endTime">The end time each day within the date range.</param>
		/// <returns>A <see cref="Timing"/> instance configured for both a fixed date range and fixed daily time intervals,
		/// meaning the action is applicable daily between the specified start and end times, from the start date to the end date.</returns>
		/// <example>
		/// The following code demonstrates how to create a <see cref="Timing"/> object for a scheduling period
		/// that applies every day from May 1, 2024, to May 10, 2024, between 8:00 AM and 4:00 PM:
		/// <code>
		/// DateOnly startDate = new DateOnly(2024, 5, 1);
		/// DateOnly endDate = new DateOnly(2024, 5, 10);
		/// TimeOnly startTime = new TimeOnly(8, 0, 0);
		/// TimeOnly endTime = new TimeOnly(16, 0, 0);
		/// Timing timing = CreateForDateFixedTimeInterval(startDate, endDate, startTime, endTime);
		/// </code>
		/// </example>
		public static Timing CreateForDateFixedTimeInterval(DateOnly startDate, DateOnly endDate, TimeOnly startTime, TimeOnly endTime)
		{
			return new()
			{
				Type = TimingType.DateFixedTimeInterval,
				StartDate = startDate,
				EndDate = endDate,
				StartTime = startTime,
				EndTime = endTime,
			};
		}
	}
}
