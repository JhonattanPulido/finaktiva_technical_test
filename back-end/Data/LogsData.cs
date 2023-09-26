using Models;
using MongoDB.Bson;
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
		/// <returns>Logs list</returns>
		public Task<List<Log>> Get(Pagination pagination);
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
		/// <returns>Logs list</returns>
		public async Task<List<Log>> Get(Pagination pagination) =>
			await LogsCollection.Find(new BsonDocument()).ToListAsync();
	}
}

