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
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Interfaces.Repository;
using System.Data;

namespace Shared.Data
{
	/// <summary>
	/// Represents an abstraction to custom EF Core <see cref="DbContext"/> implementation.
	/// </summary>
	/// <typeparam name="T">Custom implementation of <see cref="DbContext"/>.</typeparam>
	/// <param name="context">Then database context.</param>
	public class DataBase<T>(T context) : IDataBase where T : DbContext
	{
		protected readonly T _context = context ?? throw new ArgumentNullException(nameof(context));

		/// <inheritdoc/>
		public virtual IDbContextTransaction BeginTransaction()
		{
			return _context.Database.BeginTransaction();
		}

		/// <inheritdoc/>
		public virtual IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			return _context.Database.BeginTransaction(isolationLevel);
		}

		/// <inheritdoc/>
		public virtual Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
		{
			return _context.Database.BeginTransactionAsync(cancellationToken);
		}

		/// <inheritdoc/>
		public virtual Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
		{
			return _context.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
		}

		/// <inheritdoc/>
		public virtual int SaveChanges()
		{
			return _context.SaveChanges();
		}

		/// <inheritdoc/>
		public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return _context.SaveChangesAsync(cancellationToken);
		}
	}
}
