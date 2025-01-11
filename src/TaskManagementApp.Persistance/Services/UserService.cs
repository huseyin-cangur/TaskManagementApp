

using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Application.Repositories;
using TaskManagementApp.Application.Services;
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Persistance.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IList<User>> GetAll()
        {
             return await _userRepository.GetAll().ToListAsync();
        }
    }
}