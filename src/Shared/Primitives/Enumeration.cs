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

using System.Reflection;

namespace Shared.Primitives
{
	/// <summary>
	/// Represents a base class for creating strongly-typed enumerations with custom values in the system, providing common functionality for all strongly-typed enumerations types.
	/// </summary>
	/// <typeparam name="TEnum">The enumeration type that derives from this base class.</typeparam>
	/// <typeparam name="TEnumValue">The type of custom values associated with enumeration members.</typeparam>
	/// <remarks>
	/// Strongly typed enums provide a type-safe way to work with enumerated values, allowing you to define
	/// custom values associated with each enumeration member. Use this base class to create and manage
	/// strongly typed enumerations with ease.
	/// </remarks>
	public abstract class Enumeration<TEnum, TEnumValue> : IEquatable<Enumeration<TEnum, TEnumValue>> where TEnum : Enumeration<TEnum, TEnumValue>
	{
		private static readonly Dictionary<string, TEnum> Enumerations = CreateEnumerations();

		/// <summary>
		/// Gets the name of the enumeration member.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Gets the value of the enumeration member.
		/// </summary>
		public TEnumValue Value { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Enumeration{TEnum, TEnumValue}"/> class with a name and value.
		/// </summary>
		/// <param name="name">The name of the enumeration member.</param>
		/// <param name="value">The value of the enumeration member.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> or <paramref name="value"/> is <see langword="null"/> or empty.</exception>
		protected Enumeration(string name, TEnumValue value)
		{
			if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException(nameof(name));

			if (value == null)
				throw new ArgumentNullException(nameof(value));

			Name = name;
			Value = value;
		}

		/// <summary>
		/// Compares two values to determine if they are equal.
		/// </summary>
		/// <param name="first">The first custom value.</param>
		/// <param name="second">The second custom value.</param>
		/// <returns><see langword="true"/> if the custom values are equal; otherwise, <see langword="false"/>.</returns>
		protected abstract bool IsValueEqual(TEnumValue? first, TEnumValue? second);

		/// <summary>
		/// Determines whether two <see cref="Enumeration{TEnum, TEnumValue}"/> objects are equal by comparing their names and values.
		/// </summary>
		/// <param name="first">The first <see cref="Enumeration{TEnum, TEnumValue}"/> to compare.</param>
		/// <param name="second">The second <see cref="Enumeration{TEnum, TEnumValue}"/> to compare.</param>
		/// <returns><see langword="true"/> if the enumerations have the same name and value; otherwise, <see langword="false"/>.</returns>
		public static bool operator ==(Enumeration<TEnum, TEnumValue>? first, Enumeration<TEnum, TEnumValue>? second)
		{
			if (first is null && second is null)
				return true;

			if (first is null || second is null)
				return false;

			return first.Equals(second);
		}

		/// <summary>
		/// Determines whether two <see cref="Enumeration{TEnum, TEnumValue}"/> objects are not equal by comparing their names and values.
		/// </summary>
		/// <param name="first">The first <see cref="Enumeration{TEnum, TEnumValue}"/> to compare.</param>
		/// <param name="second">The second <see cref="Enumeration{TEnum, TEnumValue}"/> to compare.</param>
		/// <returns><see langword="true"/> if the enumerations have different names and/or values; otherwise, <see langword="false"/>.</returns>
		public static bool operator !=(Enumeration<TEnum, TEnumValue>? first, Enumeration<TEnum, TEnumValue>? second)
		{
			return !(first == second);
		}

		/// <summary>
		/// Determines whether the current <see cref="Enumeration{TEnum, TEnumValue}"/> object is equal to another object by comparing their names and values.
		/// </summary>
		/// <param name="obj">The object to compare with the current enumeration.</param>
		/// <returns><see langword="true"/> if the object is an <see cref="Enumeration{TEnum, TEnumValue}"/> and has the same name and value as the current enumeration; otherwise, <see langword="false"/>.</returns>
		public override bool Equals(object? obj)
		{
			return Equals(obj as Enumeration<TEnum, TEnumValue>);
		}

		/// <summary>
		/// Returns a hash code for the current <see cref="Enumeration{TEnum, TEnumValue}"/> object based on its name hash code and value hash code.
		/// </summary>
		/// <returns>A hash code for the enumeration.</returns>
		public override int GetHashCode()
		{
			return HashCode.Combine(Name.GetHashCode(), Value?.GetHashCode() ?? 0);
		}

		/// <summary>
		/// Returns the name of the current enumeration member as a string.
		/// </summary>
		public override string ToString()
		{
			return Name;
		}

		/// <summary>
		/// Determines whether the current <see cref="Enumeration{TEnum, TEnumValue}"/> object is equal to another <see cref="Enumeration{TEnum, TEnumValue}"/> object by comparing their names and values.
		/// </summary>
		/// <param name="other">The <see cref="Enumeration{TEnum, TEnumValue}"/> to compare with the current enumeration.</param>
		/// <returns><see langword="true"/> if the entities have the same name and value; otherwise, <see langword="false"/>.</returns>
		public bool Equals(Enumeration<TEnum, TEnumValue>? other)
		{
			if (other is null)
				return false;

			if (other.GetType() != GetType())
				return false;

			return other.Name == Name && IsValueEqual(other.Value, Value);
		}

		/// <summary>
		/// Retrieves an enumeration member by its name.
		/// </summary>
		/// <param name="name">The name of the enumeration member.</param>
		/// <returns>The enumeration member with the specified name, or <see langword="null"/> if not found.</returns>
		public static TEnum? FromName(string name)
		{
			return Enumerations.TryGetValue(name, out TEnum? enumeration) ? enumeration : default;
		}

		/// <summary>
		/// Retrieves an enumeration member by its value.
		/// </summary>
		/// <param name="value">The value associated with the enumeration member.</param>
		/// <returns>The enumeration member with the specified value, or <see langword="null"/> if not found.</returns>
		public static TEnum? FromValue(TEnumValue value)
		{
			return Enumerations.Values.SingleOrDefault(e => e.IsValueEqual(e.Value, value));
		}

		/// <summary>
		/// Checks if the enumeration with the specified name exists.
		/// </summary>
		/// <param name="id">The enumeration name.</param>
		/// <returns><see langword="true"/> if an enumeration with the specified name exists, otherwise <see langword="false"/>.</returns>
		public static bool Contains(string name) => Enumerations.Keys.Contains(name);

		/// <summary>
		/// Gets the enumeration values.
		/// </summary>
		/// <returns>The read-only collection of enumeration values.</returns>
		public static IReadOnlyCollection<TEnum> GetValues() => Enumerations.Values.ToList();

		/// <summary>
		/// Creates a dictionary of enumeration members based on reflection and public static readonly fields with enumeration instances of <typeparamref name="TEnum"/> enumeration type.
		/// </summary>
		/// <returns>A dictionary of enumeration members with their names as keys.</returns>
		private static Dictionary<string, TEnum> CreateEnumerations()
		{
			var enumerationType = typeof(TEnum);

			var fieldsFromType = enumerationType.GetFields(BindingFlags.Public
												 | BindingFlags.Static
												 | BindingFlags.FlattenHierarchy)
									  .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
									  .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);

			return fieldsFromType.ToDictionary(x => x.Name);
		}
	}

	/// <summary>
	/// Represents a base class for creating strongly-typed enumerations with integer values.
	/// </summary>
	/// <inheritdoc/>
	public abstract class Enumeration<TEnum> : Enumeration<TEnum, int> where TEnum : Enumeration<TEnum>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Enumeration{TEnum}"/> class with a name and value.
		/// </summary>
		/// <inheritdoc/>
		protected Enumeration(string name, int value) : base(name, value)
		{
		}

		/// <inheritdoc/>
		protected sealed override bool IsValueEqual(int first, int second)
		{
			return first == second;
		}
	}
}
