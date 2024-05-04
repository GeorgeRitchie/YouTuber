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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared.Data;
using Shared.Interfaces.Repository;

namespace Core.Downloader.Module
{
	/// <summary>
	/// Contains extension methods for the <see cref="IServiceCollection"/> interface.
	/// </summary>
	public static class DependencyInjection
	{
		/// <summary>
		/// Initializes Downloader module and adds required services into DI.
		/// </summary>
		/// <param name="services">The collection of service descriptors of DI.</param>
		/// <returns>The updated service collection.</returns>
		/// <remarks>
		/// Note: After calling AddDownloaderModule, make sure to call InitializeDownloaderModule on the service provider
		/// (<see cref="IServiceProvider"/>) to ensure the downloader module is properly initialized.
		/// </remarks>
		public static IServiceCollection AddDownloaderModule(this IServiceCollection services)
		{
			// This is required to avoid any problems with YoutubeExplode provider on any device.
			Environment.SetEnvironmentVariable("SLAVA_UKRAINI", "1");

			// Adding YouTubeDownloader
			services.AddSingleton<IDownloader, YouTubeDownloader>();

			// Adding DataBase
			services.AddDbContext<DownloaderDbContext>((serviceProvider, options) =>
			{
				var appSettings = serviceProvider.GetService<IOptions<DownloaderAppSettings>>()!.Value;
				options.UseSqlite(appSettings.DbConnectionString);
			});

			// Adding Database Interface
			services.AddScoped<IDataBase, DataBase<DownloaderDbContext>>();

			// Adding Repositories
			services.AddScoped<IRepository<ScheduledDownload>, Repository<ScheduledDownload, DownloaderDbContext>>();
			services.AddScoped<IReadOnlyRepository<ScheduledDownload>>(serviceProvider =>
													serviceProvider.GetRequiredService<IRepository<ScheduledDownload>>());
			services.AddScoped<IWriteOnlyRepository<ScheduledDownload>>(serviceProvider =>
													serviceProvider.GetRequiredService<IRepository<ScheduledDownload>>());

			return services;
		}

		/// <summary>
		/// Initializes the Downloader module after the service provider has been built.
		/// </summary>
		/// <param name="serviceProvider">The service provider.</param>
		/// <returns>The updated service provider.</returns>
		public static IServiceProvider InitializeDownloaderModule(this IServiceProvider serviceProvider)
		{
			// Create database and apply migrations if no database exists
			serviceProvider.EnsureDataBaseCreated();

			return serviceProvider;
		}

		private static void EnsureDataBaseCreated(this IServiceProvider serviceProvider)
		{
			using var scope = serviceProvider.CreateScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<DownloaderDbContext>();
			if (!dbContext.Database.CanConnect())
			{
				dbContext.Database.Migrate();
			}
		}
	}
}
