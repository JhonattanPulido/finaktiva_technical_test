namespace Models
{
	/// <summary>
	/// Pagination model
	/// </summary>
	public class Pagination
	{
		/// <summary>
		/// Page number
		/// </summary>
		public byte PageNumber { get; set; }

		/// <summary>
		/// Items per page
		/// </summary>
		public byte Quantity { get; set; }

		/// <summary>
		/// Initial date filter
		/// </summary>
		public DateTime InitialDate { get; set; }

		/// <summary>
		/// Final date filter
		/// </summary>
		public DateTime FinalDate { get; set; }

		/// <summary>
		/// Instance pagination object
		/// </summary>
		/// <param name="pageNumber">Page number</param>
		/// <param name="quantity">Items per page</param>
		/// <param name="initialDate">Initial date filter</param>
		/// <param name="finalDate">Final date filter</param>
		public Pagination(byte pageNumber, byte quantity, DateTime initialDate, DateTime finalDate)
		{
			PageNumber = pageNumber;
			Quantity = quantity;
			InitialDate = initialDate;
			FinalDate = finalDate;
		}
	}
}

