

using TaskManagementApp.Domain.Dtos;

namespace TaskManagementApp.Application.Services
{
    public interface ITaskService
    {
        IList<TaskDto> GetAll(string userId);
        Task<TaskDto>GetById(string id);
        Task<CreateTaskDto> AddAsync(CreateTaskDto dto);
        Task<UpdateTaskDto> UpdateAsync(UpdateTaskDto dto);
        Task<bool> RemoveByIdAsync(string id);
    }
}