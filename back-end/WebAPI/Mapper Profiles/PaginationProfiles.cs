using Models;
using DTOs.Input;
using AutoMapper;

namespace WebAPI.MapperProfiles
{
	/// <summary>
	/// Pagination auto-mapper profiles
	/// </summary>
	public class PaginationProfiles : Profile
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public PaginationProfiles()
		{
			CreateMap<PaginationDTO, Pagination>();
		}
	}
}

