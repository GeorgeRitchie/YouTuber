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

using Core.YouTube.Module.Entities;
using Shared.Results;

namespace Core.YouTube.Module.Services
{
	/// <summary>
	/// Represents YouTube mediator service.
	/// </summary>
	public interface IYouTubeService
	{
		/// <summary>
		/// Returns <see cref="Result{TValue}"/> with YouTube item (<see cref="IYTItem"/>) information or failure result, if something went wrong.
		/// </summary>
		/// <param name="url">The url of YouTube item.</param>
		/// <param name="cancellationToken">A token to observe while waiting for the task to complete to support cancellation.</param>
		/// <returns><see cref="Result{TValue}"/> with YouTube item (<see cref="IYTItem"/>) information or failure result, if something went wrong.</returns>
		/// <exception cref="ArgumentNullException">Thrown when url is null.</exception>
		/// <exception cref="ArgumentException">Thrown when url is <see cref="string.Empty"/> or white-space string.</exception>
		Task<Result<IYTItem>> GetItemInfoAsync(string url, CancellationToken cancellationToken = default);

		/// <summary>
		/// Returns <see cref="Result{TValue}"/> with YouTube playlist (<see cref="PlayList"/>) information or failure result, if something went wrong.
		/// </summary>
		/// <param name="url">The url of YouTube playlist.</param>
		/// <param name="cancellationToken">A token to observe while waiting for the task to complete to support cancellation.</param>
		/// <returns><see cref="Result{TValue}"/> with YouTube playlist (<see cref="PlayList"/>) information or failure result, if something went wrong.</returns>
		/// <exception cref="ArgumentNullException">Thrown when url is null.</exception>
		/// <exception cref="ArgumentException">Thrown when url is <see cref="string.Empty"/> or white-space string.</exception>
		Task<Result<PlayList>> GetPlayListInfoAsync(string url, CancellationToken cancellationToken = default);

		/// <summary>
		/// Returns <see cref="Result{TValue}"/> with YouTube video (<see cref="Video"/>) information or failure result, if something went wrong.
		/// </summary>
		/// <param name="url">The url of YouTube video.</param>
		/// <param name="cancellationToken">A token to observe while waiting for the task to complete to support cancellation.</param>
		/// <returns><see cref="Result{TValue}"/> with YouTube video (<see cref="Video"/>) information or failure result, if something went wrong.</returns>
		/// <exception cref="ArgumentNullException">Thrown when url is null.</exception>
		/// <exception cref="ArgumentException">Thrown when url is <see cref="string.Empty"/> or white-space string.</exception>
		Task<Result<Video>> GetVideoInfoAsync(string url, CancellationToken cancellationToken = default);
	}
}