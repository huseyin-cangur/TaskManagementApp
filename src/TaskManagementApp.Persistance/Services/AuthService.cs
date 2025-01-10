

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Application.Repositories;
using TaskManagementApp.Application.Services;
using TaskManagementApp.Domain.Dtos;
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Persistance.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, IMapper mapper, IRoleService roleService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task<UserDto> Login(LoginDto dto)
        {

            User user = await _userRepository.GetWhere(p => p.IdentityNumber == dto.IdentityNumber && p.Password == dto.Password).FirstOrDefaultAsync();
            bool checkIsadmin = _roleService.isAdmin(user?.Id);
            UserDto userDto = _mapper.Map<UserDto>(user);
            userDto.isAdmin = checkIsadmin;
            return userDto;

        }

    }
}