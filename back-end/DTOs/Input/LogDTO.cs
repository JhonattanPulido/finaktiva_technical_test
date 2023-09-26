using Enums;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Input
{
	/// <summary>
	/// Log data transfer object
	/// </summary>
	public class LogDTO
	{
		/// <summary>
		/// Log detailed description
		/// </summary>
		[Required(AllowEmptyStrings = false, ErrorMessage = "Log description is required")]
		[StringLength(256, MinimumLength = 4, ErrorMessage = "Log description must have min 4 and max 256 characters")]
		public string Description { get; set; }

		/// <summary>
		/// Log type
		/// </summary>
		[Required(ErrorMessage = "Log type is required")]
		public LogType Type { get; set; }

		/// <summary>
		/// Log creation date
		/// </summary>
		public DateTime? CreationDate { get; set; }

		/// <summary>
		/// Instance log DTO object
		/// </summary>
		/// <param name="description">Log detailed description</param>
		/// <param name="type">Log type</param>
		/// <param name="creationDate">Log creation date</param>
		public LogDTO(string description, LogType type, DateTime? creationDate)
		{
			Description = description;
			Type = type;
			CreationDate = creationDate;
		}
	}
}

