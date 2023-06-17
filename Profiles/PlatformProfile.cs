using AutoMapper;
using PlatformService.DTOS;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformProfile :Profile
    {
        public PlatformProfile()
        {
            // Source -> Target
            CreateMap<Platform, PlatformReadDTO>();
            CreateMap<PlatformCreateDTO, Platform>();
        }
    }
}
