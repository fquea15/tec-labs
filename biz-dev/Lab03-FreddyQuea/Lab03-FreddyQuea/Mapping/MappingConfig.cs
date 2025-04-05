using AutoMapper;
using Lab03_FreddyQuea.DTOs.Task;
using Lab03_FreddyQuea.DTOs.User;
using Lab03_FreddyQuea.Models;

namespace Lab03_FreddyQuea.Mapping;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<CreateUser, User>();
        CreateMap<CreateTask, Models.Task>();

        CreateMap<User, GetUser>();
        CreateMap<Models.Task, GetTask>();
    }
}