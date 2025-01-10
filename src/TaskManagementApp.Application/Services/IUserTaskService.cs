

using TaskManagementApp.Domain.Dtos;

namespace TaskManagementApp.Application.Services
{
    public interface IUserTaskService
    {
         Task AddAsync(CreateUserTaskDto dto);
         void RemoveUser(RemoveUserTaskDto dto);

    }
}