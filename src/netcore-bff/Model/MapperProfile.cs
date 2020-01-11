using AutoMapper;

namespace netcore_bff.Model {
#pragma warning disable 1591
    public class MapperProfile : Profile {
        public MapperProfile() {
            CreateMap<netcore_data_access.Models.RubberDuck, RubberDuck>()
                .ForMember(dest => dest.RubberDuckId, opt => opt.MapFrom(src => src.Id));
        }
    }
#pragma warning restore 1591
}