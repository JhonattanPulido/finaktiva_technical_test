namespace DTOs.Output
{
	/// <summary>
	/// Pagination output data transfer object
	/// </summary>
	public class PaginationOutputDTO<T>
	{
		/// <summary>
		/// Total pages count
		/// </summary>
		public byte Pages { get; private set; }

		/// <summary>
		/// Total items count
		/// </summary>
		public long Count { get; private set; }

		/// <summary>
		/// Items list
		/// </summary>
		public List<T> Items { get; private set; }

		/// <summary>
		/// Instance pagination output DTO
		/// </summary>
		/// <param name="count">Total items count</param>
		/// <param name="items">Items list</param>
		/// <param name="pages">Total pages count</param>
		/// <param name="pageSize">Search page size to calculate total pages quantity</param>
		public PaginationOutputDTO(long count, List<T> items, byte? pages = null, byte? pageSize = null)
		{
            Pages = pages ?? (byte)Math.Ceiling(count / (double)pageSize!);
			Count = count;
			Items = items;
		}
	}
}

