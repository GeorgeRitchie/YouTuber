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
	/// Represents repository interface with write only methods.
	/// </summary>
	/// <typeparam name="TEntity">The entity type this repository manages.</typeparam>
	public interface IWriteOnlyRepository<TEntity> where TEntity : class
	{
		/// <summary>
		/// Creates a new entity in database.
		/// </summary>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		TEntity Create(TEntity entity);

		/// <summary>
		/// Creates multiple new entities in database.
		/// </summary>
		/// <param name="entities">The entities to create.</param>
		void CreateRange(IEnumerable<TEntity> entities);

		/// <summary>
		/// Updates an existing entity in database.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		void Update(TEntity entity);

		/// <summary>
		/// Updates multiple existing entities in database.
		/// </summary>
		/// <param name="entities">The entities to update.</param>
		void UpdateRange(IEnumerable<TEntity> entities);

		/// <summary>
		/// Deletes an entity by its identifier from database.
		/// </summary>
		/// <param name="id">The identifier of the entity to delete.</param>
		void Delete(Guid id);

		/// <summary>
		/// Deletes an entity from database.
		/// </summary>
		/// <param name="entity">The entity to delete.</param>
		void Delete(TEntity entity);

		/// <summary>
		/// Deletes multiple entities from database.
		/// </summary>
		/// <param name="entities">The entities to delete.</param>
		void DeleteRange(IEnumerable<TEntity> entities);
	}
}
