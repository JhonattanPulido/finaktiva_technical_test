using Data;
using Models;
using AutoMapper;
using DTOs.Input;
using DTOs.Output;

namespace Services
{
	/// <summary>
	/// Logs services interface
	/// </summary>
	public interface ILogsServices
	{
		/// <summary>
		/// Save log
		/// </summary>
		/// <param name="log">Log data</param>
		/// <returns>Loaded log data</returns>
		public Task<LogOutputDTO> Create(LogDTO log);

		/// <summary>
		/// Get logs
		/// </summary>
		/// <param name="pagination">Pagination data</param>
		/// <returns>Logs list</returns>
		public Task<List<LogOutputDTO>> Get(PaginationDTO pagination);
	}

	/// <summary>
	/// Logs services layer
	/// </summary>
	public class LogsServices : ILogsServices
	{
		private IMapper Mapper { get; }
		private ILogsData LogsData { get; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="mapper">Injected mapper</param>
		/// <param name="logsData">Injected logs data layer</param>
		public LogsServices(IMapper mapper, ILogsData logsData)
		{
			Mapper = mapper;
			LogsData = logsData;
		}

        /// <summary>
        /// Save log
        /// </summary>
        /// <param name="log">Log data</param>
        /// <returns>Loaded log data</returns>
        public async Task<LogOutputDTO> Create(LogDTO log)
        {
			Log logData = Mapper.Map<Log>(log); // Parse log DTO to model

			logData = await LogsData.Create(log: logData); // Save data and get log metadata

			return Mapper.Map<LogOutputDTO>(logData); // Return loaded log data
        }

        /// <summary>
        /// Get logs
        /// </summary>
        /// <param name="pagination">Pagination data</param>
        /// <returns>Logs list</returns>
        public async Task<List<LogOutputDTO>> Get(PaginationDTO pagination)
		{
			Pagination paginationData = Mapper.Map<Pagination>(pagination); // Parse pagination DTO to model

			List<Log> logs = await LogsData.Get(pagination: paginationData); // Get logs list

			return Mapper.Map<List<LogOutputDTO>>(logs); // Return logs
		}
    }
}

