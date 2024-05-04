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

namespace Shared.Extensions
{
	/// <summary>
	/// Contains extension methods for the <see cref="Task"/>.
	/// </summary>
	public static class TaskExtensions
	{
		/// <summary>
		/// Executes a task asynchronously without waiting for its completion.
		/// </summary>
		/// <param name="task">The task to be executed.</param>
		/// <remarks>
		/// Intended for non-critical background operations. Suppresses and does not rethrow exceptions.
		/// </remarks>
		public static void FireAndForget(this Task task)
		{
			if (!task.IsCompleted || task.IsFaulted)
			{
				_ = ForgetAwaited(task);
			}

			async static Task ForgetAwaited(Task task)
			{
				await task.ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
			}
		}
	}
}
