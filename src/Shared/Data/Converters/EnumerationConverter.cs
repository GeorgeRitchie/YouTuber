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

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Primitives;

namespace Shared.Data.Converters
{
	/// <summary>
	/// Converts custom enumeration types implemented from <see cref="Enumeration{TEnum}"/> to and from strings for database storage.
	/// </summary>
	/// <typeparam name="T">The enumeration type that implements <see cref="Enumeration{TEnum}"/>.</typeparam>
	public sealed class EnumerationConverter<T> : ValueConverter<T, string> where T : Enumeration<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EnumerationConverter{T}"/>.
		/// </summary>
		public EnumerationConverter() : base(v => v.ToString(),
											 v => (T)typeof(T).GetMethod("FromName")!.Invoke(null, new object[] { v })!)
		{ }
	}
}
