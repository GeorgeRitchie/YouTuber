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
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces.Repository;

namespace Core.Downloader.Module.Tests
{
	public class ServiceRegistrationData : TheoryData<Type, Type, ServiceLifetime>
	{
		public ServiceRegistrationData()
		{
			Add(typeof(IDownloader), typeof(YouTubeDownloader), ServiceLifetime.Singleton);
			Add(typeof(DownloaderDbContext), typeof(DownloaderDbContext), ServiceLifetime.Scoped);
			Add(typeof(IDataBase), typeof(IDataBase), ServiceLifetime.Scoped);
			Add(typeof(IRepository<ScheduledDownload>), typeof(IRepository<ScheduledDownload>), ServiceLifetime.Scoped);
			Add(typeof(IDownloadManager), typeof(DownloadManager), ServiceLifetime.Singleton);
		}
	}

	public class DependencyInjectionTests
	{
		[Theory]
		[ClassData(typeof(ServiceRegistrationData))]
		public void AddDownloaderModule_Should_IncludeRequiredServicesCorrectly(Type serviceType, Type expectedType, ServiceLifetime expectedLifetime)
		{
			// Arrange
			IServiceCollection services = new ServiceCollection().AddLogging(configure => configure.AddConsole());
			services.Configure<DownloaderAppSettings>(appSettings => appSettings.DbConnectionString = string.Empty);

			// Act
			services.AddDownloaderModule();
			using var serviceProvider = services.BuildServiceProvider();

			var serviceInstance = serviceProvider.GetService(serviceType);
			var serviceDescriptor = services.FirstOrDefault(i => i.ServiceType == serviceType);

			// Assert
			serviceInstance.Should().BeAssignableTo(expectedType);
			serviceDescriptor?.Lifetime.Should().Be(expectedLifetime);
		}
	}
}
