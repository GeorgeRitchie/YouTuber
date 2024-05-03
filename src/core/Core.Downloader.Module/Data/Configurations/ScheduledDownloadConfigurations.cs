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
using Core.Downloader.Module.Entities;
using Core.Downloader.Module.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Data.Converters;

namespace Core.Downloader.Module.Data.Configurations
{
	/// <summary>
	/// Represents the <see cref="ScheduledDownload"/> entity configuration.
	/// </summary>
	internal sealed class ScheduledDownloadConfigurations : IEntityTypeConfiguration<ScheduledDownload>
	{
		/// <inheritdoc/>
		public void Configure(EntityTypeBuilder<ScheduledDownload> builder)
		{
			builder.ToTable(TableNames.ScheduledDownloads);

			builder.HasKey(i => i.Id);

			builder.Property(i => i.Id).ValueGeneratedNever();

			builder.Property(i => i.DownloadingType).HasConversion<EnumerationConverter<DownloadingType>>();

			builder.OwnsOne(i => i.Timing, timingNavigationBuilder =>
			{
				timingNavigationBuilder.Property(o => o.Type).HasConversion<EnumerationConverter<TimingType>>();
			});
		}
	}
}
