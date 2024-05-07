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

using Core.Downloader.Module.Entities;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.Interfaces.Repository;

namespace Core.Downloader.Module.Services
{
	/// <summary>
	/// Manages scheduled downloads.
	/// </summary>
	/// <param name="dataBase">Database context.</param>
	/// <param name="repository">Repository for handling <see cref="ScheduledDownload"/> entities.</param>
	/// <param name="logger">Logger for logging operations.</param>
	/// <exception cref="ArgumentNullException">Thrown if any parameter is <see langword="null"/>.</exception>
	internal sealed class ScheduledDownloadManager(IDataBase dataBase, IRepository<ScheduledDownload> repository, ILogger<ScheduledDownloadManager> logger)
	{
		private readonly ILogger<ScheduledDownloadManager> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
		private readonly IDataBase _dataBase = dataBase ?? throw new ArgumentNullException(nameof(dataBase));
		private readonly IRepository<ScheduledDownload> _repository = repository ?? throw new ArgumentNullException(nameof(repository));

		/// <summary>
		/// Gets a list of all scheduled downloads.
		/// </summary>
		public IReadOnlyList<ScheduledDownload> ScheduledItems => _repository.GetAllAsNoTracking().ToList();

		/// <summary>
		/// Schedules a new download.
		/// </summary>
		/// <param name="item">The download item to schedule.</param>
		/// <param name="cancellationToken">Token for cancelling the operation.</param>
		/// <exception cref="ArgumentNullException">Thrown if the <paramref name="item"/> is <see langword="null"/>.</exception>
		public async Task ScheduleDownloadAsync(ScheduledDownload item, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(item, nameof(item));

			using var transaction = await _dataBase.BeginTransactionAsync(cancellationToken);

			try
			{
				_repository.Create(item);
				await _dataBase.SaveChangesAsync(cancellationToken);
				await transaction.CommitAsync(cancellationToken);

				_logger.LogFormattedDebug($"{nameof(DownloadManager)}.{nameof(ScheduleDownloadAsync)}",
										"Scheduled download was setup.",
										item);
			}
			catch (Exception ex)
			{
				_logger.LogFormattedError($"{nameof(DownloadManager)}.{nameof(ScheduleDownloadAsync)}",
										"Something went wrong while scheduling new download.",
										ex);

				await transaction.RollbackAsync(cancellationToken);

				throw;
			}
		}

		/// <summary>
		/// Cancels a scheduled download.
		/// </summary>
		/// <param name="id">The identifier of the download to cancel.</param>
		/// <param name="cancellationToken">Token for cancelling the operation.</param>
		public async Task CancelScheduleAsync(Guid id, CancellationToken cancellationToken = default)
		{
			var transaction = _dataBase.BeginTransaction();

			try
			{
				_repository.Delete(id);
				await _dataBase.SaveChangesAsync(cancellationToken);
				await transaction.CommitAsync(cancellationToken);

				_logger.LogFormattedDebug($"{nameof(DownloadManager)}.{nameof(CancelScheduleAsync)}",
										"Scheduled download was removed by user.",
										id);
			}
			catch (Exception ex)
			{
				_logger.LogFormattedError($"{nameof(DownloadManager)}.{nameof(CancelScheduleAsync)}",
										"Something went wrong while removing schedule.",
										ex);

				await transaction.RollbackAsync(cancellationToken);

				throw;
			}
		}
	}
}