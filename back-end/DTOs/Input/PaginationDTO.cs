using System.ComponentModel.DataAnnotations;

namespace DTOs.Input
{
	/// <summary>
	/// Pagination data transfer object
	/// </summary>
	public class PaginationDTO : IValidatableObject
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
		[Required(ErrorMessage = "Quantity query parameter is required")]
        [Range(1, 255, ErrorMessage = "Quantity must be between 1 and 255")]
        public byte Quantity { get; set; }

		/// <summary>
		/// Initial date filter
		/// </summary>
		[Required(ErrorMessage = "Initial date query parameter is required")]
		[DataType(DataType.Date)]
		public DateTime InitialDate { get; set; }

		/// <summary>
		/// Final date filter
		/// </summary>
		[Required(ErrorMessage = "Final date query parameter is required")]
        [DataType(DataType.Date)]
        public DateTime FinalDate { get; set; }

		/// <summary>
		/// Instance pagination DTO object
		/// </summary>
		public PaginationDTO()
		{

		}

		/// <summary>
		/// Custom initial and final date validation
		/// </summary>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
			if (FinalDate < InitialDate)
				yield return new ValidationResult("Final date must be greater than initial date", new[] { "FinalDate" });
        }
    }
}

