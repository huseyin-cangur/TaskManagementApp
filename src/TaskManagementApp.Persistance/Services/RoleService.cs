

using TaskManagementApp.Application.Repositories;
using TaskManagementApp.Application.Services;

namespace TaskManagementApp.Persistance.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public RoleService(IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public bool isAdmin(string userId)
        {
            var userRoles = from userRole in _userRoleRepository.GetAll()
                            join role in _roleRepository.GetAll() on userRole.RoleId equals role.Id
                            where userRole.UserId == userId
                            select role;

            if (userRoles.Any(p => p.Name == "Admin"))
                return true;
                
            return false;

        }
    }
}