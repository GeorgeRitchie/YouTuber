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

using Shared.Errors;

namespace Core.YouTube.Module.Errors
{
	/// <summary>
	/// Represents a wrapper for all errors in YouTube module.
	/// </summary>
	public static class YouTubeModuleErrors
	{
		/// <summary>
		/// Represents an invalid url error.
		/// </summary>
		/// <param name="details">Description for error.</param>
		/// <returns>An instance of <see cref="Error"/>.</returns>
		public static Error InvalidUrlError(string details) => new("InvalidUrl", details);
	}
}
