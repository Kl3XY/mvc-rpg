using AutoMapper;

namespace API
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<mvc_rpg.Entities.Enemy, Models.Enemy>()
                .ReverseMap();
        }
    }
}
