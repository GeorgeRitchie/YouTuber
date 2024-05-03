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

using Microsoft.EntityFrameworkCore;
using Shared.Interfaces.Repository;

namespace Shared.Data
{
	/// <summary>
	/// Represents repository pattern for <typeparamref name="T"/> entity.
	/// </summary>
	/// <typeparam name="T">The entity type.</typeparam>
	/// <typeparam name="K">Custom implementation of <see cref="DbContext"/>.</typeparam>
	/// <param name="context">Then database context.</param>
	public class Repository<T, K>(K context) : IRepository<T>, IWriteOnlyRepository<T>, IReadOnlyRepository<T> where T : class where K : DbContext
	{
		protected readonly DbSet<T> dbSet = context?.Set<T>() ?? throw new ArgumentNullException(nameof(context));

		/// <inheritdoc/>
		public virtual T Create(T entity)
		{
			return dbSet.Add(entity).Entity;
		}

		/// <inheritdoc/>
		public virtual void CreateRange(IEnumerable<T> entities)
		{
			dbSet.AddRange(entities);
		}

		/// <inheritdoc/>
		public virtual void Update(T entity)
		{
			dbSet.Update(entity);
		}

		/// <inheritdoc/>
		public virtual void UpdateRange(IEnumerable<T> entities)
		{
			dbSet.UpdateRange(entities);
		}

		/// <inheritdoc/>
		public virtual void Delete(Guid id)
		{
			Delete(dbSet.Find(id)!);
		}

		/// <inheritdoc/>
		public virtual void Delete(T entity)
		{
			dbSet.Remove(entity);
		}

		/// <inheritdoc/>
		public virtual void DeleteRange(IEnumerable<T> entities)
		{
			dbSet.RemoveRange(entities);
		}

		/// <inheritdoc/>
		public virtual IQueryable<T> GetAll()
		{
			return dbSet;
		}

		/// <inheritdoc/>
		public virtual IQueryable<T> GetAllIgnoringQueryFilters()
		{
			return dbSet.IgnoreQueryFilters();
		}

		/// <inheritdoc/>
		public virtual IQueryable<T> GetAllAsNoTracking()
		{
			return dbSet.AsNoTracking();
		}

		/// <inheritdoc/>
		public virtual IQueryable<T> GetAllIgnoringQueryFiltersAsNoTracking()
		{
			return dbSet.IgnoreQueryFilters().AsNoTracking();
		}
	}
}
