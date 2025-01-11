
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Application.Services
{
    public interface IUserService
    {
        Task<IList<User>> GetAll();
    }
}