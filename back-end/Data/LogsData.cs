using Models;
using DTOs.Input;
using DTOs.Output;
using MongoDB.Driver;

namespace Data
{
	/// <summary>
	/// Logs data interface
	/// </summary>
	public interface ILogsData
	{
		/// <summary>
		/// Save log
		/// </summary>
		/// <param name="log">Log data</param>
		/// <returns>Charged log data</returns>
		public Task<Log> Create(Log log);

        /// <summary>
        /// Get logs
        /// </summary>
        /// <param name="pagination">Pagination data</param>
        /// <returns>Paginated logs</returns>
        public Task<PaginationOutputDTO<Log>> Get(PaginationDTO pagination);
	}

	/// <summary>
	/// Logs data layer
	/// </summary>
	public class LogsData : ILogsData
	{
		/// <summary>
		/// Logs collection variable
		/// </summary>
		private IMongoCollection<Log> LogsCollection { get; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="logsCollection">Injected logs collection</param>
		public LogsData(IMongoCollection<Log> logsCollection)
		{
			LogsCollection = logsCollection;
		}

        /// <summary>
        /// Save log
        /// </summary>
        /// <param name="log">Log data</param>
        /// <returns>Charged log data</returns>
        public async Task<Log> Create(Log log)
		{
			await LogsCollection.InsertOneAsync(log);
			return log;
		}

		/// <summary>
		/// Get logs
		/// </summary>
		/// <param name="pagination">Pagination data</param>
		/// <returns>Paginated logs</returns>
		public async Task<PaginationOutputDTO<Log>> Get(PaginationDTO pagination)
		{
			FilterDefinitionBuilder<Log> filterBuilder = Builders<Log>.Filter;

			// Set dates ranges
			FilterDefinition<Log> filter =
				filterBuilder.Gte(x => x.CreationDate, pagination.InitialDate) &
				filterBuilder.Lte(x => x.CreationDate, pagination.FinalDate?.AddDays(1));

            List<Log> logs = await LogsCollection
				.Find(pagination.Type.HasValue ?
					filter & filterBuilder.Eq(x => x.Type, pagination.Type) :
					filter)
				.SortByDescending(x => x.CreationDate)
				.Skip((pagination.PageNumber - 1) * pagination.PageSize)
				.Limit(pagination.PageSize)
				.ToListAsync(); // Get paginated logs

			long count = await LogsCollection.CountDocumentsAsync(pagination.Type.HasValue ?
                    filter & filterBuilder.Eq(x => x.Type, pagination.Type) :
                    filter); // Get logs count

			return new(count: count, items: logs, pageSize: pagination.PageSize);
		}
	}
}

