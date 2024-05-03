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

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Core.Downloader.Module.Data
{
	/// <summary>
	/// Provides a factory for creating and configuring instances of <see cref="DownloaderDbContext"/>
	/// for design-time services like migrations.
	/// </summary>
	internal sealed class DownloaderDbContextFactory : IDesignTimeDbContextFactory<DownloaderDbContext>
	{
		/// <inheritdoc/>
		public DownloaderDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<DownloaderDbContext>();

			// TODO try to get db connection string from configs
			optionsBuilder.UseSqlite("Data Source=youtuber.db");

			return new DownloaderDbContext(optionsBuilder.Options);
		}
	}
}
