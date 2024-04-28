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
	/// Represents a result of some operation, with status information and possibly an error.
	/// </summary>
	public class Result
	{
		protected bool _status;
		protected readonly List<Error> _errors;

		/// <summary>
		/// Gets a value indicating whether the result is a success.
		/// </summary>
		public bool IsSuccess => _status;

		/// <summary>
		/// Gets a value indicating whether the result is a failure.
		/// </summary>
		public bool IsFailure => !_status;

		/// <summary>
		/// Gets an immutable collection of errors associated with the failure operation or empty collection for successful operation.
		/// </summary>
		public IReadOnlyCollection<Error> Errors => _errors;

		/// <summary>
		/// Initializes a new instance of the <see cref="Result"/> class with the specified parameters.
		/// </summary>
		/// <param name="isSuccess">The flag indicating if the result is successful.</param>
		/// <param name="error">An optional error message. Should be <see langword="null"/> for successful result and not <see langword="null"/> for failure result.</param>
		/// <exception cref="ArgumentException">Thrown when the combination of <paramref name="isSuccess"/> and <paramref name="error"/> is inappropriate.</exception>
		protected Result(bool isSuccess, Error? error)
		{
			if (isSuccess == true && error != Error.None)
				throw new ArgumentException($"Inappropriate values of '{nameof(isSuccess)}' and '{nameof(error)}'");

			if (isSuccess == false && (error == null || error == Error.None))
				throw new ArgumentException($"Inappropriate values of '{nameof(isSuccess)}' and '{nameof(error)}'");

			_status = isSuccess;
			_errors = isSuccess == true ? new() : new() { error! };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Result"/> class with the specified parameters.
		/// </summary>
		/// <param name="isSuccess">A flag indicating whether the result is successful or failure.</param>
		/// <param name="errors">A collection of error messages. Should be empty collection for successful result and not empty collection for failure result.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="errors"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown when the combination of <paramref name="isSuccess"/> and <paramref name="errors"/> is inappropriate.</exception>
		protected Result(bool isSuccess, IEnumerable<Error> errors)
		{
			if (errors is null)
				throw new ArgumentNullException(nameof(errors));

			if (isSuccess == true && errors.Any() == true)
				throw new ArgumentException($"Inappropriate values of '{nameof(isSuccess)}' and '{nameof(errors)}'");

			if (isSuccess == false && errors.Any() == false)
				throw new ArgumentException($"Inappropriate values of '{nameof(isSuccess)}' and '{nameof(errors)}'");

			_status = isSuccess;
			_errors = errors.ToList();
		}

		/// <summary>
		/// Returns a success <see cref="Result"/>.
		/// </summary>
		/// <returns>A new instance of <see cref="Result"/>.</returns>
		public static Result Success() => new(true, Error.None);

		/// <summary>
		/// Returns a failure <see cref="Result"/> with a default error message.
		/// </summary>
		/// <returns>A new instance of <see cref="Result"/> with a default error message.</returns>
		public static Result Failure() => new(false, Error.Default);

		/// <summary>
		/// Returns a failure <see cref="Result"/> with the specified error.
		/// </summary>
		/// <param name="error">The error.</param>
		/// <returns>A new instance of <see cref="Result"/> with the specified error.</returns>
		public static Result Failure(Error error) => new(false, error);

		/// <summary>
		/// Returns a failure <see cref="Result"/> with multiple errors.
		/// </summary>
		/// <param name="errors">A collection of errors associated with the failure.</param>
		/// <returns>A new instance of <see cref="Result"/> with the specified errors.</returns>
		public static Result Failure(IEnumerable<Error> errors) => new(false, errors);

		/// <summary>
		/// Returns a success <see cref="Result{TValue}"/> with the specified value.
		/// </summary>
		/// <typeparam name="TValue">The result type.</typeparam>
		/// <param name="value">The result value.</param>
		/// <returns>A new instance of <see cref="Result{TValue}"/> with the specified value.</returns>
		public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

		/// <summary>
		/// Returns a failure <see cref="Result{TValue}"/> with the specified value and a default error message.
		/// </summary>
		/// <typeparam name="TValue">The type of value associated with the result.</typeparam>
		/// <param name="value">The value associated with the failed result.</param>
		/// <returns>A new instance of <see cref="Result{TValue}"/> with the specified value and a default error message.</returns>
		public static Result<TValue> Failure<TValue>(TValue? value) => new(value, false, Error.Default);

		/// <summary>
		/// Returns a failure <see cref="Result{TValue}"/> with the specified error.
		/// </summary>
		/// <typeparam name="TValue">The result type.</typeparam>
		/// <param name="error">The error.</param>
		/// <returns>A new instance of <see cref="Result{TValue}"/> with the specified error and failure flag set.</returns>
		public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);

		/// <summary>
		/// Returns a failure <see cref="Result{TValue}"/> with the specified value and error message.
		/// </summary>
		/// <typeparam name="TValue">The type of value associated with the result.</typeparam>
		/// <param name="value">The value associated with the failed result.</param>
		/// <param name="error">The error message for the failed result.</param>
		/// <returns>A new instance of <see cref="Result{TValue}"/> with the specified value and error message.</returns>
		public static Result<TValue> Failure<TValue>(TValue? value, Error error) => new(value, false, error);

		/// <summary>
		/// Returns a failure <see cref="Result{TValue}"/> with the specified value and a collection of error messages.
		/// </summary>
		/// <typeparam name="TValue">The type of value associated with the result.</typeparam>
		/// <param name="value">The value associated with the failed result.</param>
		/// <param name="errors">A collection of error messages for the failed result.</param>
		/// <returns>A new instance of <see cref="Result{TValue}"/> with the specified value and a collection of error messages.</returns>
		public static Result<TValue> Failure<TValue>(TValue? value, IEnumerable<Error> errors) => new(value, false, errors);

		/// <summary>
		/// Returns a success <see cref="Result"/> if condition <see langword="true"/>, otherwise failure <see cref="Result"/> with <see cref="Error.ConditionNotMet"/> error.
		/// </summary>
		/// <param name="condition">The condition.</param>
		/// <returns>A new instance of <see cref="Result"/>.</returns>
		public static Result Create(bool condition) => condition ? Success() : Failure(Error.ConditionNotMet);

		/// <summary>
		/// Returns a success <see cref="Result{TValue}"/> with the specified nullable value, when value is not equal to <see langword="null"/>, otherwise failure <see cref="Result{TValue}"/> with <see cref="Error.NullValue"/> error.
		/// </summary>
		/// <typeparam name="TValue">The result type.</typeparam>
		/// <param name="value">The result value.</param>
		/// <returns>A new instance of <see cref="Result{TValue}"/> with the specified value or an error.</returns>
		public static Result<TValue> Create<TValue>(TValue? value) => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);

		/// <summary>
		/// Returns the first failure from the specified <paramref name="results"/>.
		/// If there is no failure, a success is returned.
		/// </summary>
		/// <param name="results">The results array.</param>
		/// <returns>
		/// The first failure from the specified <paramref name="results"/> array, or a success if it does not exist.
		/// </returns>
		public static async Task<Result> FirstFailureOrSuccess(params Func<Task<Result>>[] results)
		{
			foreach (Func<Task<Result>> resultTask in results)
			{
				Result result = await resultTask();

				if (result.IsFailure)
				{
					return result;
				}
			}

			return Success();
		}

		/// <summary>
		/// Combines multiple <see cref="Result"/> instances into a single result.
		/// </summary>
		/// <param name="results">An array of results to combine.</param>
		/// <returns>An new instance <see cref="Result"/> representing the combined result with success status if all results are succeed, otherwise, with failure status and corresponding errors if any result is failure.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="results"/> is <see langword="null"/> or empty.</exception>
		public static Result Combine(params Result[] results)
		{
			if (results == null || results.Any() == false)
				throw new ArgumentNullException(nameof(results));

			if (results.Any(i => i.IsFailure))
				return Failure(results.SelectMany(r => r.Errors).Distinct());

			return Result.Success();
		}
	}
}
