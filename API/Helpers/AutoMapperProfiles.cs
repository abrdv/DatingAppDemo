using API.DTOs;
using API.Entities;
using API.Extentions;
using AutoMapper;


namespace API.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.CalculateAge()))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain)!.PhotoUrl));
            CreateMap<Photo, PhotoDto>();
            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<RegisterDto, AppUser>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.Username.ToLower()));
            CreateMap<string, DateOnly>()
                .ConvertUsing(s => DateOnly.Parse(s));
        }

    }
}
