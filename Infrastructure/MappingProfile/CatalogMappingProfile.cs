using Application.Catalogs.CatalogTypes;
using AutoMapper;
using Domain.Catalogs;

namespace Infrastructure.MappingProfile
{
    public class CatalogMappingProfile:Profile
    {
        public CatalogMappingProfile()
        {
            CreateMap<CatalogType, CatalogTypeDto>().ReverseMap();

            CreateMap<CatalogType, CatalogTypeListDto>()
                .ForMember(x => x.SubTypeCount,
                    x => x.MapFrom(x => x.SubType.Count));
        }
    }
}
