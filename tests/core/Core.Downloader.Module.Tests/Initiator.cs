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

using Core.Downloader.Module.Entities;
using Core.Downloader.Module.Enumerations;
using Microsoft.Extensions.Logging;

namespace Core.Downloader.Module.Tests
{
	internal static class Initiator
	{
		public static MediaStream CreateNewAudioOnlyMediaStream()
		{
			return new MediaStream(2, "mp3", "128 Kbit/s", StreamType.AudioOnly, CreateNewMediaFile());
		}

		public static MediaFile CreateNewMediaFile()
		{
			return new MediaFile("example", "https://example.com", "Example", "Example Example", "Example example", new(), new(), null);
		}

		public static ILogger<T> CreateConsoleLogger<T>()
		{
			var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
			var logger = loggerFactory.CreateLogger<T>();
			return logger;
		}
	}
}
