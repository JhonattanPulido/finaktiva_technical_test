using Models;
using DTOs.Input;
using AutoMapper;
using DTOs.Output;

namespace WebAPI.MapperProfiles
{
	/// <summary>
	/// Log auto-mapper profiles
	/// </summary>
	public class LogProfiles : Profile
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public LogProfiles()
		{
			CreateMap<LogDTO, Log>()
				.ForMember(ds => ds.Id, op => op.Ignore())
				.ForMember(ds => ds.CreationDate, op => op.MapFrom(src => src.CreationDate ?? DateTime.Now));

			CreateMap<Log, LogOutputDTO>();
		}
	}
}

