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
		/// <returns>Paginated logs</returns>
		public Task<PaginationOutputDTO<LogOutputDTO>> Get(PaginationDTO pagination);
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
		/// <returns>Paginated logs</returns>
		public async Task<PaginationOutputDTO<LogOutputDTO>> Get(PaginationDTO pagination)
        {
			PaginationOutputDTO<Log> data = await LogsData.Get(pagination: pagination); // Get logs list

			List<LogOutputDTO> logs = Mapper.Map<List<LogOutputDTO>>(data.Items);

			return new(count: data.Count, items: logs, pages: data.Pages);
		}
    }
}

