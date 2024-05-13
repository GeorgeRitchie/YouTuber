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

namespace Shared.Primitives
{
	/// <summary>
	/// Represents the abstract value object primitive.
	/// </summary>
	public abstract class ValueObject : IEquatable<ValueObject>
	{
		public static bool operator ==(ValueObject? a, ValueObject? b)
		{
			if (a is null && b is null)
			{
				return true;
			}

			if (a is null || b is null)
			{
				return false;
			}

			return a.Equals(b);
		}

		public static bool operator !=(ValueObject? a, ValueObject? b) => !(a == b);

		/// <inheritdoc />
		public virtual bool Equals(ValueObject? other) => other is not null && ValuesAreEqual(other);

		/// <inheritdoc />
		public override bool Equals(object? obj) => obj is ValueObject valueObject && ValuesAreEqual(valueObject);

		/// <inheritdoc />
		public override int GetHashCode() =>
			GetAtomicValues().Aggregate(default(int), (hashcode, value) => HashCode.Combine(hashcode, value.GetHashCode()));

		/// <summary>
		/// Gets the atomic values of the value object.
		/// </summary>
		/// <returns>The collection of objects representing the value object values.</returns>
		protected abstract IEnumerable<object> GetAtomicValues();

		private bool ValuesAreEqual(ValueObject valueObject) => GetAtomicValues().SequenceEqual(valueObject.GetAtomicValues());
	}
}
