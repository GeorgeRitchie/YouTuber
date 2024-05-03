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

using Core.Downloader.Module.Data.Constants;
using Microsoft.EntityFrameworkCore;

namespace Core.Downloader.Module.Data
{
	// add-migration Init -context DownloaderDbContext -o Data/Migrations
	// update-database -context DownloaderDbContext
	// migration -context DownloaderDbContext
	// remove-migration -context DownloaderDbContext
	// drop-database -context DownloaderDbContext

	/// <summary>
	/// Represents the downloader module database context.
	/// </summary>
	/// <param name="options">The database context options.</param>
	internal sealed class DownloaderDbContext(DbContextOptions<DownloaderDbContext> options) : DbContext(options)
	{
		/// <inheritdoc/>
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		}

		/// <inheritdoc/>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.HasDefaultSchema(Schemas.Downloader);

			modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
		}
	}
}
