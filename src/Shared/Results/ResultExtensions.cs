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

using Shared.Errors;

namespace Shared.Results
{
	/// <summary>
	/// Contains extension methods for working with the <see cref="Result"/> class.
	/// </summary>
	public static class ResultExtensions
	{
		/// <summary>
		/// Maps the success result based on the specified mapping function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The mapping function.</param>
		/// <returns>The mapped result.</returns>
		public static Result<TOut> Map<TOut>(this Result result, Func<TOut> func) =>
			result.IsSuccess ? func() : Result.Failure<TOut>(default, result.Errors);

		/// <summary>
		/// Maps the success result based on the specified mapping function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="resultTask">The result task.</param>
		/// <param name="func">The mapping function.</param>
		/// <returns>The mapped result.</returns>
		public static async Task<Result<TOut>> Map<TOut>(this Task<Result> resultTask, Func<TOut> func) =>
			(await resultTask).Map(func);

		/// <summary>
		/// Maps the success result based on the specified mapping function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The mapping function.</param>
		/// <returns>The mapped result.</returns>
		public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> func) =>
			result.IsSuccess ? func(result.Value) : Result.Failure<TOut>(default, result.Errors);

		/// <summary>
		/// Maps the success result based on the specified mapping function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="resultTask">The result task.</param>
		/// <param name="func">The mapping function.</param>
		/// <returns>The mapped result.</returns>
		public static async Task<Result<TOut>> Map<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, TOut> func) =>
			(await resultTask).Map(func);

		/// <summary>
		/// Maps the failure result based on the specified error function, otherwise returns a success result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The error function.</param>
		/// <returns>The mapped result.</returns>
		public static Result<TIn> MapFailure<TIn>(this Result<TIn> result, Func<Error> func) =>
			result.IsSuccess ? result : Result.Failure<TIn>(func());

		/// <summary>
		/// Maps the failure result based on the specified error function, otherwise returns a success result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="resultTask">The result task.</param>
		/// <param name="func">The error function.</param>
		/// <returns>The mapped result.</returns>
		public static async Task<Result<TIn>> MapFailure<TIn>(this Task<Result<TIn>> resultTask, Func<Error> func) =>
			(await resultTask).MapFailure(func);

		/// <summary>
		/// Binds the success result based on the specified binding function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The binding function.</param>
		/// <returns>The bound result.</returns>
		public static Result Bind<TIn>(this Result<TIn> result, Func<TIn, Result> func) =>
			result.IsSuccess ? func(result.Value) : Result.Failure(result.Errors);

		/// <summary>
		/// Binds the success result based on the specified binding function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The binding function.</param>
		/// <returns>The bound result.</returns>
		public static async Task<Result> Bind<TIn>(this Task<Result<TIn>> result, Func<TIn, Result> func) =>
			(await result).Bind(func);

		/// <summary>
		/// Binds the success result based on the specified binding function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The binding function.</param>
		/// <returns>The bound result.</returns>
		public static async Task<Result> Bind<TIn>(this Result<TIn> result, Func<TIn, Task<Result>> func) =>
			result.IsSuccess ? await func(result.Value) : Result.Failure(result.Errors);

		/// <summary>
		/// Binds the success result based on the specified binding function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The binding function.</param>
		/// <returns>The bound result.</returns>
		public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> func) =>
			result.IsSuccess ? func(result.Value) : Result.Failure<TOut>(default, result.Errors);

		/// <summary>
		/// Binds the success result based on the specified binding function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The binding function.</param>
		/// <returns>The bound result.</returns>
		public static async Task<Result<TOut>> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Task<Result<TOut>>> func) =>
			result.IsSuccess ? await func(result.Value) : Result.Failure<TOut>(default, result.Errors);

		/// <summary>
		/// Binds the success result based on the specified binding function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The binding function.</param>
		/// <returns>The bound result.</returns>
		public static async Task<Result<TOut>> Bind<TOut>(this Result result, Func<Task<Result<TOut>>> func) =>
			result.IsSuccess ? await func() : Result.Failure<TOut>(default, result.Errors);

		/// <summary>
		/// Binds the success result based on the specified binding function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="resultTask">The result task.</param>
		/// <param name="func">The binding function.</param>
		/// <returns>The bound result.</returns>
		public static async Task<Result<TOut>> Bind<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, Result<TOut>> func) =>
			(await resultTask).Bind(func);

		/// <summary>
		/// Binds the success result based on the specified binding function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="resultTask">The result task.</param>
		/// <param name="func">The binding function.</param>
		/// <returns>The bound result.</returns>
		public static async Task<Result<TOut>> Bind<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, Task<Result<TOut>>> func) =>
			await (await resultTask).Bind(func);

		/// <summary>
		/// Tap will execute the provided action if the result is successful.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="func">The function.</param>
		/// <returns>The same result.</returns>
		public static async Task<Result> Tap(this Result result, Func<Task> func)
		{
			if (result.IsSuccess)
			{
				await func();
			}

			return result;
		}

		/// <summary>
		/// Tap will execute the provided action if the result is successful.
		/// </summary>
		/// <param name="resultTask">The result task.</param>
		/// <param name="func">The function.</param>
		/// <returns>The same result.</returns>
		public static async Task<Result> Tap(this Task<Result> resultTask, Func<Task> func) => await (await resultTask).Tap(func);

		/// <summary>
		/// Tap will execute the provided action if the result is successful.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="action">The action.</param>
		/// <returns>The same result.</returns>
		public static Result<TIn> Tap<TIn>(this Result<TIn> result, Action<TIn> action)
		{
			if (result.IsSuccess)
			{
				action(result.Value);
			}

			return result;
		}

		/// <summary>
		/// Tap will execute the provided action if the result is successful.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="resultTask">The result task.</param>
		/// <param name="func">The function.</param>
		/// <returns>The same result.</returns>
		public static async Task<Result<TIn>> Tap<TIn>(this Task<Result<TIn>> resultTask, Func<Task> func) => await (await resultTask).Tap(func);

		/// <summary>
		/// Tap will execute the provided action if the result is successful.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="resultTask">The result task.</param>
		/// <param name="action">The action.</param>
		/// <returns>The same result.</returns>
		public static async Task<Result<TIn>> Tap<TIn>(this Task<Result<TIn>> resultTask, Action<TIn> action) => (await resultTask).Tap(action);

		/// <summary>
		/// Tap will execute the provided action if the result is successful.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The function.</param>
		/// <returns>The same result.</returns>
		public static async Task<Result<TIn>> Tap<TIn>(this Result<TIn> result, Func<Task> func)
		{
			if (result.IsSuccess)
			{
				await func();
			}

			return result;
		}

		/// <summary>
		/// On failure will execute the provided action if the result is a failure.
		/// </summary>
		/// <param name="resultTask">The result task.</param>
		/// <param name="action">The action.</param>
		/// <returns>The same result.</returns>
		public static async Task<Result> OnFailure(this Task<Result> resultTask, Action<IReadOnlyCollection<Error>> action)
		{
			Result result = await resultTask;

			if (result.IsFailure)
			{
				action(result.Errors);
			}

			return result;
		}

		/// <summary>
		/// On failure will execute the provided action if the result is a failure.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="resultTask">The result task.</param>
		/// <param name="action">The action.</param>
		/// <returns>The same result.</returns>
		public static async Task<Result<TIn>> OnFailure<TIn>(this Task<Result<TIn>> resultTask, Action<IReadOnlyCollection<Error>> action)
		{
			Result<TIn> result = await resultTask;

			if (result.IsFailure)
			{
				action(result.Errors);
			}

			return result;
		}

		/// <summary>
		/// Filter will return the success result if the specified predicate evaluates to true.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="predicate">The predicate.</param>
		/// <returns>The same result if the specified predicate evaluates to true.</returns>
		public static Result<TIn> Filter<TIn>(this Result<TIn> result, Func<TIn, bool> predicate)
		{
			if (result.IsFailure)
			{
				return result;
			}

			return predicate(result.Value) ? result : Result.Failure<TIn>(Error.ConditionNotMet);
		}

		/// <summary>
		/// This procedure ensures compliance with a predetermined condition. Upon compliance, the original <see cref="Result{TIn}"/> instance is returned. In cases of non-compliance, a failure <see cref="Result{TIn}"/> is returned, encapsulating the defined error along with any errors present in the initial <see cref="Result{TIn}"/> instance.
		/// </summary>
		/// <param name="predicate">The predicate to satisfy.</param>
		/// <param name="error">The error message for non-compliance case.</param>
		/// <returns>An instance of <see cref="Result{TIn}"/> with status satisfied to predicate result.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="result"/>, <paramref name="predicate"/> or <paramref name="error"/> is <see langword="null"/>.</exception>
		public static Result<TIn> Ensure<TIn>(this Result<TIn> result, Func<TIn?, bool> predicate, Error error)
		{
			if (result == null) throw new ArgumentNullException(nameof(result));

			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			if (error == null) throw new ArgumentNullException(nameof(error));

			if (predicate.Invoke(result.Value) == false)
			{
				return Result.Failure(result.Value, [.. result.Errors, error]);
			}

			return result;
		}

		/// <summary>
		/// Ensures that the operation result satisfies a collection of predicates, and if not, returns failure <see cref="Result{TIn}"/> with corresponding error messages.
		/// </summary>
		/// <param name="validators">An array of predicate and error message pairs to validate the result.</param>
		/// <returns>A new instance of <see cref="Result{TIn}"/> with value of current instance with succeed status if all predicates returned <see langword="true"/> or failure status with corresponding errors when any predicate returned <see langword="false"/>.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="result"/> is <see langword="null"/> or <paramref name="validators"/> is <see langword="null"/> or empty.</exception>
		public static Result<TIn> Ensure<TIn>(this Result<TIn> result, params (Func<TIn?, bool> predicate, Error error)[] validators)
		{
			if (result is null)
				throw new ArgumentNullException(nameof(result));

			if (validators == null || validators.Any() == false)
				throw new ArgumentNullException(nameof(validators));

			var results = new List<Result<TIn>>();

			foreach (var (predicate, error) in validators)
			{
				results.Add(result.Ensure(predicate, error));
			}

			return Result<TIn>.Combine([.. results]);
		}

		/// <summary>
		/// This procedure ensures compliance with a predetermined condition. Upon compliance, the original <see cref="Result"/> instance is returned. In cases of non-compliance, a failure <see cref="Result"/> is returned, encapsulating the defined error along with any errors present in the initial <see cref="Result"/> instance.
		/// </summary>
		/// <param name="predicate">The predicate to satisfy.</param>
		/// <param name="error">The error message for non-compliance case.</param>
		/// <returns>An instance of <see cref="Result"/> with status satisfied to predicate result.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="result"/>, <paramref name="predicate"/> or <paramref name="error"/> is <see langword="null"/>.</exception>
		public static Result Ensure(this Result result, Func<bool> predicate, Error error)
		{
			if (result == null) throw new ArgumentNullException(nameof(result));

			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			if (error == null) throw new ArgumentNullException(nameof(error));

			if (predicate.Invoke() == false)
			{
				return Result.Failure([.. result.Errors, error]);
			}

			return result;
		}

		/// <summary>
		/// Ensures that the operation result satisfies a collection of predicates, and if not, returns failure <see cref="Result"/> with corresponding error messages.
		/// </summary>
		/// <param name="validators">An array of predicate and error message pairs to validate the result.</param>
		/// <returns>A new instance of <see cref="Result"/> with value of current instance with succeed status if all predicates returned <see langword="true"/> or failure status with corresponding errors when any predicate returned <see langword="false"/>.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="result"/> is <see langword="null"/> or <paramref name="validators"/> is <see langword="null"/> or empty.</exception>
		public static Result Ensure(this Result result, params (Func<bool> predicate, Error error)[] validators)
		{
			if (result is null)
				throw new ArgumentNullException(nameof(result));

			if (validators == null || validators.Any() == false)
				throw new ArgumentNullException(nameof(validators));

			var results = new List<Result>();

			foreach (var (predicate, error) in validators)
			{
				results.Add(result.Ensure(predicate, error));
			}

			return Result.Combine([.. results]);
		}
	}
}
