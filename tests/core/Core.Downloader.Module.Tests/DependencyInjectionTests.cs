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
using Core.Downloader.Module.Data;
using Core.Downloader.Module.Entities;
using Core.Downloader.Module.Services;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Interfaces.Repository;

namespace Core.Downloader.Module.Tests
{
	public class DependencyInjectionTests
	{
		[Fact]
		public void AddDownloaderModule_Should_IncludeRequiredServicesCorrectly()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection().AddLogging(configure => configure.AddConsole());
			services.Configure<DownloaderAppSettings>(appSettings => appSettings.DbConnectionString = string.Empty);

			// Act
			services.AddDownloaderModule();
			using var serviceProvider = services.BuildServiceProvider();

			var downloaderService = serviceProvider.GetService<IDownloader>();
			var downloaderServiceDescriptor = services.FirstOrDefault(i => i.ServiceType == typeof(IDownloader));

			var dbContext = serviceProvider.GetService<DownloaderDbContext>();
			var dbContextDescriptor = services.FirstOrDefault(i => i.ServiceType == typeof(DownloaderDbContext));

			var database = serviceProvider.GetService<IDataBase>();
			var databaseDescriptor = services.FirstOrDefault(i => i.ServiceType == typeof(IDataBase));

			var scheduledDownloadRepository = serviceProvider.GetService<IRepository<ScheduledDownload>>();
			var scheduledDownloadRepositoryDescriptor = services.FirstOrDefault(i =>
																	i.ServiceType == typeof(IRepository<ScheduledDownload>));

			// Assert
			downloaderService.Should().BeAssignableTo<YouTubeDownloader>();
			downloaderServiceDescriptor?.Lifetime.Should().Be(ServiceLifetime.Singleton);

			dbContext.Should().BeAssignableTo<DownloaderDbContext>();
			dbContextDescriptor?.Lifetime.Should().Be(ServiceLifetime.Scoped);

			database.Should().BeAssignableTo<IDataBase>();
			databaseDescriptor?.Lifetime.Should().Be(ServiceLifetime.Scoped);

			scheduledDownloadRepository.Should().BeAssignableTo<IRepository<ScheduledDownload>>();
			scheduledDownloadRepositoryDescriptor?.Lifetime.Should().Be(ServiceLifetime.Scoped);
		}
	}
}
