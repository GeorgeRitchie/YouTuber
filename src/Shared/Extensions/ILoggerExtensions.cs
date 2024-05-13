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

using Microsoft.Extensions.Logging;

namespace Shared.Extensions
{
	/// <summary>
	/// Contains extension methods for the <see cref="ILogger"/> interface.
	/// </summary>
	public static class ILoggerExtensions
	{
		/// <summary>
		/// Logs a trace-level message with predefined format: {Module} - {Message} - {@Data}
		/// </summary>
		/// <typeparam name="T">The category of the logger.</typeparam>
		/// <typeparam name="K">The type of data to be logged.</typeparam>
		/// <param name="logger">The logger instance used for logging.</param>
		/// <param name="moduleName">The name of the part of code where the log is generated.</param>
		/// <param name="message">The message to be logged.</param>
		/// <param name="data">The data related to the log entry (structured data).</param>
		public static void LogFormattedTrace<T, K>(this ILogger<T> logger, string moduleName, string message, K data)
		{
			logger.LogTrace("{Module} - {Message} - {@Data}", moduleName, message, data);
		}

		/// <summary>
		/// Logs a trace-level message with predefined format: {Module} - {Message}
		/// </summary>
		/// <typeparam name="T">The category of the logger.</typeparam>
		/// <param name="logger">The logger instance used for logging.</param>
		/// <param name="moduleName">The name of the part of code where the log is generated.</param>
		/// <param name="message">The message to be logged.</param>
		public static void LogFormattedTrace<T>(this ILogger<T> logger, string moduleName, string message)
		{
			logger.LogTrace("{Module} - {Message}", moduleName, message);
		}

		/// <summary>
		/// Logs a debug-level message with predefined format: {Module} - {Message} - {@Data}
		/// </summary>
		/// <typeparam name="T">The category of the logger.</typeparam>
		/// <typeparam name="K">The type of data to be logged.</typeparam>
		/// <param name="logger">The logger instance used for logging.</param>
		/// <param name="moduleName">The name of the part of code where the log is generated.</param>
		/// <param name="message">The message to be logged.</param>
		/// <param name="data">The data related to the log entry (structured data).</param>
		public static void LogFormattedDebug<T, K>(this ILogger<T> logger, string moduleName, string message, K data)
		{
			logger.LogDebug("{Module} - {Message} - {@Data}", moduleName, message, data);
		}

		/// <summary>
		/// Logs a debug-level message with predefined format: {Module} - {Message}
		/// </summary>
		/// <typeparam name="T">The category of the logger.</typeparam>
		/// <param name="logger">The logger instance used for logging.</param>
		/// <param name="moduleName">The name of the part of code where the log is generated.</param>
		/// <param name="message">The message to be logged.</param>
		public static void LogFormattedDebug<T>(this ILogger<T> logger, string moduleName, string message)
		{
			logger.LogDebug("{Module} - {Message}", moduleName, message);
		}

		/// <summary>
		/// Logs a information-level message with predefined format: {Module} - {Message} - {@Data}
		/// </summary>
		/// <typeparam name="T">The category of the logger.</typeparam>
		/// <typeparam name="K">The type of data to be logged.</typeparam>
		/// <param name="logger">The logger instance used for logging.</param>
		/// <param name="moduleName">The name of the part of code where the log is generated.</param>
		/// <param name="message">The message to be logged.</param>
		/// <param name="data">The data related to the log entry (structured data).</param>
		public static void LogFormattedInformation<T, K>(this ILogger<T> logger, string moduleName, string message, K data)
		{
			logger.LogInformation("{Module} - {Message} - {@Data}", moduleName, message, data);
		}

		/// <summary>
		/// Logs a information-level message with predefined format: {Module} - {Message}
		/// </summary>
		/// <typeparam name="T">The category of the logger.</typeparam>
		/// <param name="logger">The logger instance used for logging.</param>
		/// <param name="moduleName">The name of the part of code where the log is generated.</param>
		/// <param name="message">The message to be logged.</param>
		public static void LogFormattedInformation<T>(this ILogger<T> logger, string moduleName, string message)
		{
			logger.LogInformation("{Module} - {Message}", moduleName, message);
		}

		/// <summary>
		/// Logs a warning-level message with predefined format: {Module} - {Message} - {@Data}
		/// </summary>
		/// <typeparam name="T">The category of the logger.</typeparam>
		/// <typeparam name="K">The type of data to be logged.</typeparam>
		/// <param name="logger">The logger instance used for logging.</param>
		/// <param name="moduleName">The name of the part of code where the log is generated.</param>
		/// <param name="message">The message to be logged.</param>
		/// <param name="data">The data related to the log entry (structured data).</param>
		public static void LogFormattedWarning<T, K>(this ILogger<T> logger, string moduleName, string message, K data)
		{
			logger.LogWarning("{Module} - {Message} - {@Data}", moduleName, message, data);
		}

		/// <summary>
		/// Logs a warning-level message with predefined format: {Module} - {Message}
		/// </summary>
		/// <typeparam name="T">The category of the logger.</typeparam>
		/// <param name="logger">The logger instance used for logging.</param>
		/// <param name="moduleName">The name of the part of code where the log is generated.</param>
		/// <param name="message">The message to be logged.</param>
		public static void LogFormattedWarning<T>(this ILogger<T> logger, string moduleName, string message)
		{
			logger.LogWarning("{Module} - {Message}", moduleName, message);
		}

		/// <summary>
		/// Logs a error-level message with predefined format: {Module} - {Message} - {@Data}
		/// </summary>
		/// <typeparam name="T">The category of the logger.</typeparam>
		/// <typeparam name="K">The type of data to be logged.</typeparam>
		/// <param name="logger">The logger instance used for logging.</param>
		/// <param name="moduleName">The name of the part of code where the log is generated.</param>
		/// <param name="message">The message to be logged.</param>
		/// <param name="data">The data related to the log entry (structured data).</param>
		public static void LogFormattedError<T, K>(this ILogger<T> logger, string moduleName, string message, K data)
		{
			logger.LogError("{Module} - {Message} - {@Data}", moduleName, message, data);
		}

		/// <summary>
		/// Logs a error-level message with predefined format: {Module} - {Message}
		/// </summary>
		/// <typeparam name="T">The category of the logger.</typeparam>
		/// <param name="logger">The logger instance used for logging.</param>
		/// <param name="moduleName">The name of the part of code where the log is generated.</param>
		/// <param name="message">The message to be logged.</param>
		public static void LogFormattedError<T>(this ILogger<T> logger, string moduleName, string message)
		{
			logger.LogError("{Module} - {Message}", moduleName, message);
		}

		/// <summary>
		/// Logs a critical-level message with predefined format: {Module} - {Message} - {@Data}
		/// </summary>
		/// <typeparam name="T">The category of the logger.</typeparam>
		/// <typeparam name="K">The type of data to be logged.</typeparam>
		/// <param name="logger">The logger instance used for logging.</param>
		/// <param name="moduleName">The name of the part of code where the log is generated.</param>
		/// <param name="message">The message to be logged.</param>
		/// <param name="data">The data related to the log entry (structured data).</param>
		public static void LogFormattedCritical<T, K>(this ILogger<T> logger, string moduleName, string message, K data)
		{
			logger.LogCritical("{Module} - {Message} - {@Data}", moduleName, message, data);
		}

		/// <summary>
		/// Logs a critical-level message with predefined format: {Module} - {Message}
		/// </summary>
		/// <typeparam name="T">The category of the logger.</typeparam>
		/// <param name="logger">The logger instance used for logging.</param>
		/// <param name="moduleName">The name of the part of code where the log is generated.</param>
		/// <param name="message">The message to be logged.</param>
		public static void LogFormattedCritical<T>(this ILogger<T> logger, string moduleName, string message)
		{
			logger.LogCritical("{Module} - {Message}", moduleName, message);
		}
	}
}
