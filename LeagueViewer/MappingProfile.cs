using AutoMapper;
using LeagueViewer.Models;

namespace LeagueViewer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // League -> LeagueNavigation
            CreateMap<League, LeagueNavigation>()
                .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.CountryFlag, opts => opts.MapFrom(src => src.Flag))
                .ForMember(dest => dest.LeagueId, opts => opts.MapFrom(src => src.Id));
        }
    }
}
