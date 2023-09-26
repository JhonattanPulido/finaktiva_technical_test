using Enums;

namespace DTOs.Output
{
	/// <summary>
	/// Log output data transfer object
	/// </summary>
	public class LogOutputDTO
	{
		/// <summary>
		/// Log ID
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// Log detailed description
		/// </summary>
		public string Description { get; private set; }

		/// <summary>
		/// Log type
		/// </summary>
		public LogType Type { get; private set; }

		/// <summary>
		/// Log creation date-time
		/// </summary>
		public DateTime CreationDate { get; private set; }

		/// <summary>
		/// Instance log output DTO object
		/// </summary>
		/// <param name="id">Log ID</param>
		/// <param name="description">Log detailed description</param>
		/// <param name="type">Log type</param>
		/// <param name="creationDate">Log creation date and time</param>
		public LogOutputDTO(string id, string description, LogType type, DateTime creationDate)
		{
			Id = id;
			Description = description;
			Type = type;
			CreationDate = creationDate;
		}
	}
}

