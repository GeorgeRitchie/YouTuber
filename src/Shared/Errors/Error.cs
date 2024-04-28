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

namespace Shared.Errors
{

	/// <summary>
	/// Represents an error.
	/// </summary>
	public class Error : IEquatable<Error>
	{
		/// <summary>
		/// A special value indicating no error (<see langword="null"/>).
		/// </summary>
		public static readonly Error? None = null;

		/// <summary>
		/// The default error with empty code and message.
		/// </summary>
		public static readonly Error Default = new(string.Empty, string.Empty);

		/// <summary>
		/// The null value error instance.
		/// </summary>
		public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");

		/// <summary>
		/// The condition not met error instance.
		/// </summary>
		public static readonly Error ConditionNotMet = new("Error.ConditionNotMet", "The specified condition was not met.");

		/// <summary>
		/// Gets the error code.
		/// </summary>
		public string Code { get; }

		/// <summary>
		/// Gets the error message.
		/// </summary>
		public string Message { get; }

		/// <summary>
		/// Gets the error description (optional).
		/// </summary>
		public string? Description { get; } = null;

		/// <summary>
		/// Gets the error source (optional).
		/// </summary>
		public string? Source { get; } = null;

		/// <summary>
		/// Gets the inner error (optional).
		/// </summary>
		public Error? InnerError { get; } = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Error"/> class with the specified code and message.
		/// </summary>
		/// <param name="code">The error code.</param>
		/// <param name="message">The error message.</param>
		public Error(string code, string message)
		{
			Code = code;
			Message = message;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Error"/> class with the specified properties.
		/// </summary>
		/// <param name="code">The error code.</param>
		/// <param name="message">The error message.</param>
		/// <param name="description">The error description (optional).</param>
		/// <param name="source">The error source (optional).</param>
		/// <param name="innerError">The inner error (optional).</param>
		public Error(string code, string message, string? description = null, string? source = null, Error? innerError = null) : this(code, message)
		{
			Description = description;
			Source = source;
			InnerError = innerError;
		}

		public static implicit operator string(Error error) => error.Code;

		/// <summary>
		/// Determines whether two error instances are equal by comparing their properties.
		/// </summary>
		/// <param name="first">The first error to compare.</param>
		/// <param name="second">The second error to compare.</param>
		/// <returns><see langword="true"/> if the errors are equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator ==(Error? first, Error? second)
		{
			if (first is null && second is null)
				return true;

			if (first is null || second is null)
				return false;

			return first.Equals(second);
		}

		/// <summary>
		/// Determines whether two error instances are not equal by comparing their properties.
		/// </summary>
		/// <param name="first">The first error to compare.</param>
		/// <param name="second">The second error to compare.</param>
		/// <returns><see langword="true"/> if the errors are not equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator !=(Error? first, Error? second)
		{
			return !(first == second);
		}

		/// <inheritdoc />
		public virtual bool Equals(Error? other)
		{
			if (other is null)
				return false;

			if (other.GetType() != GetType())
				return false;

			if (other.Code != Code)
				return false;

			if (other.Message != Message)
				return false;

			if (other.Description != Description)
				return false;

			if (other.Source != Source)
				return false;

			if (other.InnerError != InnerError)
				return false;

			return true;
		}

		/// <inheritdoc />
		public override bool Equals(object? obj) => obj is Error error && Equals(error);

		/// <inheritdoc />
		public override int GetHashCode()
		{
			unchecked
			{
				int hashCode = (Code != null) ? Code.GetHashCode() : 0;
				hashCode = (hashCode * 397) ^ ((Message != null) ? Message.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ ((Description != null) ? Description.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ ((Source != null) ? Source.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ ((InnerError != null) ? InnerError.GetHashCode() : 0);
				return hashCode;
			}
		}

		/// <inheritdoc />
		public override string ToString() => Code;
	}
}
