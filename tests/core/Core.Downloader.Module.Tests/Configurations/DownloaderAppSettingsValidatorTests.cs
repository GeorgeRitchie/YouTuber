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

namespace Core.Downloader.Module.Tests.Configurations
{
	internal class DownloaderAppSettingsInvalidInputsForFailureResult : TheoryData<DownloaderAppSettings>
	{
		public DownloaderAppSettingsInvalidInputsForFailureResult()
		{
			// When DbConnectionString is null
			Add(new()
			{
				DbConnectionString = null!,
			});

			// When DbConnectionString is string.Empty
			Add(new()
			{
				DbConnectionString = string.Empty,
			});

			// When DbConnectionString is white-space string
			Add(new()
			{
				DbConnectionString = "     ",
			});
		}
	}

	public sealed class DownloaderAppSettingsValidatorTests
	{
		[Theory]
		[ClassData(typeof(DownloaderAppSettingsInvalidInputsForFailureResult))]
		public void Validation_Should_Fail_WhenInvalidDataPassed(DownloaderAppSettings input)
		{
			// Arrange
			var validator = new DownloaderAppSettingsValidator();

			// Act
			var result = validator.Validate(input);

			// Assert
			result.IsValid.Should().BeFalse();
		}

		[Fact]
		public void Validation_Should_Success_WhenValidDataPassed()
		{
			// Arrange
			var validator = new DownloaderAppSettingsValidator();
			var appSettings = new DownloaderAppSettings()
			{
				DbConnectionString = "some value",
			};

			// Act
			var result = validator.Validate(appSettings);

			// Assert
			result.IsValid.Should().BeTrue();
		}
	}
}
