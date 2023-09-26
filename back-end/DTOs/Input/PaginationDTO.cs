using Enums;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Input
{
	/// <summary>
	/// Pagination data transfer object
	/// </summary>
	public class PaginationDTO
	{
		/// <summary>
		/// Page number
		/// </summary>
		[Required(ErrorMessage = "Page number query parameter is required")]
		[Range(1, 255, ErrorMessage = "Page number must be between 1 and 255")]
		public byte PageNumber { get; set; }

		/// <summary>
		/// Items per page
		/// </summary>
		[Required(ErrorMessage = "Page size query parameter is required")]
        [Range(1, 255, ErrorMessage = "Page size must be between 1 and 255")]
        public byte PageSize { get; set; }

		/// <summary>
		/// Log type
		/// </summary>
		public LogType? Type { get; set; }

		/// <summary>
		/// Initial date filter
		/// </summary>
		[Required(ErrorMessage = "Initial date query parameter is required")]
		[DataType(DataType.Date)]
		public DateTime? InitialDate { get; set; }

		/// <summary>
		/// Final date filter
		/// </summary>
		[Required(ErrorMessage = "Final date query parameter is required")]
        [DataType(DataType.Date)]
        public DateTime? FinalDate { get; set; }

		/// <summary>
		/// Instance pagination DTO object
		/// </summary>
		public PaginationDTO()
		{

		}

		/// <summary>
		/// Validate initial and final dates
		/// </summary>
		/// <returns>Final date is less than initial date?</returns>
		public bool ValidateDates() =>
			FinalDate < InitialDate;
    }
}

