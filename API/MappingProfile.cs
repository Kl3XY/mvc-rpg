using AutoMapper;

namespace API
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<mvc_rpg.Entities.Enemy, Models.Enemy>()
                .ReverseMap();
            CreateMap<mvc_rpg.Entities.Player, Models.Player>()
                .ReverseMap();
            CreateMap<mvc_rpg.Entities.Item, Models.Item>()
                .ReverseMap();
            CreateMap<mvc_rpg.Entities.ItemType, Models.ItemType>()
                .ReverseMap();
            CreateMap<mvc_rpg.Entities.EnemyType, Models.EnemyType>()
                .ReverseMap();
            CreateMap<mvc_rpg.Entities.Grave, Models.Grave>()
                .ReverseMap();
        }
    }
}
