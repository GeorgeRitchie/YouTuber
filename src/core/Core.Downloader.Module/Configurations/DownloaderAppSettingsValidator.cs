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

using FluentValidation;

namespace Core.Downloader.Module.Configurations
{
	/// <summary>
	/// Represents validator for <see cref="DownloaderAppSettings"/>.
	/// </summary>
	public sealed class DownloaderAppSettingsValidator : AbstractValidator<DownloaderAppSettings>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DownloaderAppSettingsValidator"/> class.
		/// </summary>
		public DownloaderAppSettingsValidator()
		{
			RuleFor(i => i.DbConnectionString).NotEmpty();
		}
	}
}
