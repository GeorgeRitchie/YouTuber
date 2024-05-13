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

using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Shared.Interfaces.Repository
{
	/// <summary>
	/// Represents database interface.
	/// </summary>
	public interface IDataBase
	{
		/// <summary>
		/// Starts a new database transaction.
		/// </summary>
		/// <returns>The transaction initiated.</returns>
		IDbContextTransaction BeginTransaction();

		/// <summary>
		/// Asynchronously starts a new database transaction.
		/// </summary>
		/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
		/// <returns>The transaction initiated.</returns>
		Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Starts a new database transaction with the specified isolation level.
		/// </summary>
		/// <param name="isolationLevel">The isolation level of the transaction.</param>
		/// <returns>The transaction initiated.</returns>
		IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel);

		/// <summary>
		/// Asynchronously starts a new database transaction with the specified isolation level.
		/// </summary>
		/// <param name="isolationLevel">The isolation level of the transaction.</param>
		/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
		/// <returns>The transaction initiated.</returns>
		Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default);

		/// <summary>
		/// Saves all changes made in this context to the database.
		/// </summary>
		/// <returns>The number of state entries written to the database.</returns>
		int SaveChanges();

		/// <summary>
		/// Asynchronously saves all changes made in this context to the database.
		/// </summary>
		/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
		/// <returns>The number of state entries written to the database.</returns>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
