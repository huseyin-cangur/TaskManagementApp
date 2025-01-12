
using AutoMapper;
using TaskManagementApp.Domain.Dtos;
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Persistance.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateTaskDto, Domain.Entities.Task>().ReverseMap();
            CreateMap<CreateUserTaskDto, UserTask>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<TaskDto, Domain.Entities.Task>().ReverseMap();
            CreateMap<UpdateTaskDto, Domain.Entities.Task>().ReverseMap();
        }
    }
}