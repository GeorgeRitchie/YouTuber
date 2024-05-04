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

using System.Text.RegularExpressions;

namespace Shared.Helper
{
	/// <summary>
	/// Provides utilities to create and ensure filenames are compliant with filesystem rules and unique within their directories.
	/// </summary>
	public static class FileNameFormatter
	{
		/// <summary>
		/// Modifies a filename to remove or replace characters that are not allowed in file names.
		/// This method throws an exception if the input is null, empty or consists only of whitespace.
		/// </summary>
		/// <param name="name">The filename to adjust.</param>
		/// <returns>A filename stripped of invalid characters.</returns>
		/// <exception cref="ArgumentNullException">Thrown if the <paramref name="name"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown if the <paramref name="name"/> is <see cref="string.Empty"/> or white-space string.</exception>
		public static string MakeValidFileName(string name)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

			string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
			string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);
			return Regex.Replace(name, invalidRegStr, "_");
		}

		/// <summary>
		/// Shortens a filename to ensure it does not exceed a specified maximum length, preserving the file extension.
		/// This method throws an exception if the input is null or if the specified length is out of range (1 to 250 characters).
		/// </summary>
		/// <param name="name">The filename to shorten.</param>
		/// <param name="maxLength">The maximum allowable length for the filename, not exceeding 250 characters.</param>
		/// <returns>A potentially shortened version of the original filename.</returns>
		/// <exception cref="ArgumentNullException">Thrown if the <paramref name="name"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown if the <paramref name="name"/> is <see cref="string.Empty"/> or white-space string.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="maxLength"/> is less than 1 or greater than 250.</exception>
		public static string TruncateFileName(string name, int maxLength = 220)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
			ArgumentOutOfRangeException.ThrowIfGreaterThan(maxLength, 250, nameof(maxLength));
			ArgumentOutOfRangeException.ThrowIfLessThan(maxLength, 1, nameof(maxLength));

			if (name.Length <= maxLength) return name;

			string extension = Path.GetExtension(name);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(name);
			int length = maxLength - extension.Length;
			return string.Concat(fileNameWithoutExtension.AsSpan(0, length), extension);
		}

		/// <summary>
		/// Adjusts and potentially shortens a filename to ensure it is valid and adheres to filesystem length restrictions.
		/// This method throws an exception if the input is null, empty or consists only of whitespace.
		/// </summary>
		/// <param name="name">The input string to convert into a compliant filename.</param>
		/// <returns>A filename that is both compliant with filesystem rules and within length limits.</returns>
		/// <exception cref="ArgumentNullException">Thrown if the <paramref name="name"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown if the <paramref name="name"/> is <see cref="string.Empty"/> or white-space string.</exception>
		public static string CreateSafeFileName(string name)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

			string validName = MakeValidFileName(name);
			return TruncateFileName(validName);
		}

		/// <summary>
		/// Generates a filename that is unique within a specified directory by appending a numeric suffix if necessary.
		/// This method throws an exception if any input is null, empty or consists only of whitespace,
		/// or if there are issues accessing the filesystem.
		/// </summary>
		/// <param name="directoryPath">The path to the directory where the filename will be used.</param>
		/// <param name="name">The initial filename proposal before adjustment and shortening.</param>
		/// <returns>A unique filename path within the specified directory, ensuring no conflicts with existing files.</returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown if the <paramref name="directoryPath"/> or <paramref name="name"/> is <see langword="null"/>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// Thrown if the <paramref name="directoryPath"/> or <paramref name="name"/> is <see cref="string.Empty"/>
		/// or white-space string.
		/// </exception>
		/// <exception cref="IOException">Thrown if an I/O error occurs while checking for existing files.</exception>
		public static string CreateUniqueSafeFileName(string directoryPath, string name)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(directoryPath, nameof(directoryPath));
			ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

			string safeName = CreateSafeFileName(name);
			string path = Path.Combine(directoryPath, safeName);
			string directory = Path.GetDirectoryName(path)!;
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
			string extension = Path.GetExtension(path);
			int count = 1;

			while (File.Exists(path))
			{
				string tempFileName = $"{fileNameWithoutExtension} ({count++}){extension}";
				path = Path.Combine(directory, tempFileName);
			}

			return path;
		}
	}
}
