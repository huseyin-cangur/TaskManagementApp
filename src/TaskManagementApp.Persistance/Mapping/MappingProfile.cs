
using AutoMapper;
using TaskManagementApp.Domain.Dtos;

namespace TaskManagementApp.Persistance.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateTaskDto,Domain.Entities.Task>().ReverseMap();
        }
    }
}