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
using Microsoft.Extensions.Logging;

namespace Core.YouTube.Module.Tests
{
	public class DependencyInjectionTests
	{
		[Fact]
		public void AddYouTubeModule_Should_IncludeRequiredServicesCorrectly()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection().AddLogging(configure => configure.AddConsole());

			// Act
			services.AddYouTubeModule();
			using var serviceProvider = services.BuildServiceProvider();

			var youtubeService = serviceProvider.GetService<IYouTubeService>();
			var youtubeServiceDescriptor = services.FirstOrDefault(i => i.ServiceType == typeof(IYouTubeService));

			// Assert
			youtubeService.Should().BeAssignableTo<YouTubeService>();
			youtubeServiceDescriptor?.Lifetime.Should().Be(ServiceLifetime.Singleton);
		}
	}
}
