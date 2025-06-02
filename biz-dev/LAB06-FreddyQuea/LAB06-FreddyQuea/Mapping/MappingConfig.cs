using AutoMapper;
using LAB06_FreddyQuea.DTOs.Auth;
using LAB06_FreddyQuea.Models;

namespace LAB06_FreddyQuea.Mapping;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<RegisterRequest, User>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Password,
                opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

        CreateMap<LoginRequest, User>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        CreateMap<ChangePasswordRequest, User>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        CreateMap<User, RegisterResponse>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
    }
}