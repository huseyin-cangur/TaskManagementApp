

using TaskManagementApp.Domain.Dtos;

namespace TaskManagementApp.Application.Services
{
    public interface ITaskService
    {
        IList<Domain.Entities.Task> GetAll(string userId);
        Task<Domain.Entities.Task>GetById(string id);
        Task<CreateTaskDto> AddAsync(CreateTaskDto dto);
        void UpdateAsync(UpdateTaskDto dto);
        Task RemoveByIdAsync(string id);
    }
}