using AutoMapper;
using DTOLayer.DTOs.AnnouncementDTOs;
using DTOLayer.DTOs.AppUserDTOs;
using DTOLayer.DTOs.CityDTOs;
using EntityLayer.Concrete;

namespace TraversalCoreProje.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AppUserLoginDTOs, AppUser>().ReverseMap();
            CreateMap<AppUserRegisterDTOs, AppUser>().ReverseMap();
            CreateMap<AppUserLoginDTOs, AppUser>().ReverseMap();
        }
    }
}
