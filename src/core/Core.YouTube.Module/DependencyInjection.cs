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

using Core.YouTube.Module.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core.YouTube.Module
{
	/// <summary>
	/// Contains extension methods for the <see cref="IServiceCollection"/> interface.
	/// </summary>
	public static class DependencyInjection
	{
		/// <summary>
		/// Initializes YouTube module and adds required services into DI.
		/// </summary>
		/// <param name="services">The collection of service descriptors of DI.</param>
		/// <returns>The updated service collection.</returns>
		public static IServiceCollection AddYouTubeModule(this IServiceCollection services)
		{
			// Adding YouTubeService
			services.AddSingleton<IYouTubeService, YouTubeService>();

			return services;
		}
	}
}
