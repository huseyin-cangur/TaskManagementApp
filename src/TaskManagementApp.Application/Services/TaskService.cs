

using TaskManagementApp.Domain.Dtos;

namespace TaskManagementApp.Application.Services
{
    public interface ITaskService
    {
        Task AddAsync(CreateTaskDto dto);
        void UpdateAsync(UpdateTaskDto dto);
        Task RemoveByIdAsync(string id);
    }
}