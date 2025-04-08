using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entitities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
            .ForMember(d => d.Age, o => o.MapFrom (s => s.DateOfBirth.CalculateAge()))
            .ForMember(d => d.PhotoURL, o =>
            o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain)!.Url));  //Since Photo URL is not matched so added manually to map. will return null if not found.
            CreateMap<Photo, PhotoDto>();


        }
    }
}