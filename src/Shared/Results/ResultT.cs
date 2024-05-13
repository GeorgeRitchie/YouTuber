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
	/// Represents the result of some operation, with status information and possibly a value and an error.
	/// </summary>
	/// <typeparam name="TValue">The result value type.</typeparam>
	public class Result<TValue> : Result
	{
		private readonly TValue? _value;

		/// <summary>
		/// Gets the value associated with the operation result when status of operation result is succeed, for failure operation result returns default value for type <typeparamref name="TValue"/>
		/// </summary>
		public TValue? Value => IsSuccess == true ? _value : default;

		/// <summary>
		/// Initializes a new instance of the <see cref="Result{TValueType}"/> class with the specified parameters.
		/// </summary>
		/// <param name="value">The result value.</param>
		/// <param name="isSuccess">The flag indicating if the result is successful.</param>
		/// <param name="error">The error.</param>
		protected internal Result(TValue? value, bool isSuccess, Error error)
			: base(isSuccess, error) =>
			_value = value;

		/// <summary>
		/// Initializes a new instance of the <see cref="Result{TValueType}"/> class with the specified parameters.
		/// </summary>
		/// <param name="value">The result value.</param>
		/// <inheritdoc cref="Result.Result(bool, IEnumerable{Error})"/>
		protected internal Result(TValue? value, bool isSuccess, IEnumerable<Error> errors) : base(isSuccess, errors)
		{
			_value = value;
		}

		/// <summary>
		/// Implicitly converts a nullable value of type <typeparamref name="TValue"/> to an <see cref="Result{TValue}"/>.
		/// If the value is <see langword="null"/>, a failure <see cref="Result{TValue}"/> with <see cref="Error.NullValue"/> error is created; otherwise, a success <see cref="Result{TValue}"/> is created.
		/// </summary>
		/// <param name="value">The nullable value to be converted to a result.</param>
		/// <returns>An <see cref="Result{TValue}"/> representing the converted value.</returns>
		public static implicit operator Result<TValue>(TValue? value) => Create(value);

		/// <summary>
		/// Implicitly converts an <see cref="Result{TValue}"/> to a nullable value of type <typeparamref name="TValue"/>.
		/// If the result is successful, the associated value is returned; otherwise, <see langword="null"/> is returned.
		/// </summary>
		/// <param name="result">The result to be converted to a nullable value.</param>
		/// <returns>The value associated with the result if it is successful; otherwise, <see langword="null"/>.</returns>
		public static implicit operator TValue?(Result<TValue> result) => result.Value;

		/// <summary>
		/// Combines multiple <see cref="Result{TValue}"/> instances into a single result.
		/// </summary>
		/// <param name="results">An array of results to combine.</param>
		/// <returns>An new instance <see cref="Result{TValue}"/> representing the combined result with value from first instance and success status if all results are succeed, otherwise, with failure status and corresponding errors if any result is failure.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="results"/> is <see langword="null"/> or empty.</exception>
		public static Result<TValue> Combine(params Result<TValue>[] results)
		{
			if (results == null || results.Any() == false)
				throw new ArgumentNullException(nameof(results));

			if (results.Any(i => i.IsFailure))
				return Failure(results[0].Value, results.SelectMany(r => r.Errors).Distinct());

			return Success(results[0].Value);
		}
	}
}
