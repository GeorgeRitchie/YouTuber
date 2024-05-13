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

namespace Shared.Interfaces.Repository
{
	/// <summary>
	/// Represents repository interface with read only methods.
	/// </summary>
	/// <typeparam name="TEntity">The entity type this repository manages.</typeparam>
	public interface IReadOnlyRepository<TEntity> where TEntity : class
	{
		/// <summary>
		/// Retrieves all entities from database.
		/// </summary>
		/// <returns>An <see cref="IQueryable"/> of <typeparamref name="TEntity"/> representing all entities in database.</returns>
		IQueryable<TEntity> GetAll();

		/// <summary>
		/// Retrieves all entities from database without tracking changes.
		/// </summary>
		/// <returns>An <see cref="IQueryable"/> of <typeparamref name="TEntity"/> representing all entities in database with no change tracking.</returns>
		IQueryable<TEntity> GetAllAsNoTracking();

		/// <summary>
		/// Retrieves all entities from database, ignoring any configured query filters.
		/// </summary>
		/// <returns>An <see cref="IQueryable"/> of <typeparamref name="TEntity"/> representing all entities ignoring query filters.</returns>
		IQueryable<TEntity> GetAllIgnoringQueryFilters();

		/// <summary>
		/// Retrieves all entities from database, ignoring any configured query filters and without tracking changes.
		/// </summary>
		/// <returns>An <see cref="IQueryable"/> of <typeparamref name="TEntity"/> representing all entities ignoring query filters and with no change tracking.</returns>
		IQueryable<TEntity> GetAllIgnoringQueryFiltersAsNoTracking();
	}
}
