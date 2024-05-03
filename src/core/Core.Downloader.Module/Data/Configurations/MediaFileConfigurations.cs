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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Downloader.Module.Data.Configurations
{
	/// <summary>
	/// Represents the <see cref="MediaFile"/> entity configuration.
	/// </summary>
	internal sealed class MediaFileConfigurations : IEntityTypeConfiguration<MediaFile>
	{
		/// <inheritdoc/>
		public void Configure(EntityTypeBuilder<MediaFile> builder)
		{
			builder.ToTable(TableNames.MediaFiles);

			builder.HasKey(i => i.Id);

			builder.Property(i => i.Id).ValueGeneratedNever();

			builder.OwnsOne(i => i.Thumbnail);
		}
	}
}
