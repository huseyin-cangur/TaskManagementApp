


using TaskManagementApp.Domain.Dtos;

namespace TaskManagementApp.Application.Services
{
    public interface IAuthService
    {
        Task<UserDto> Login(LoginDto dto);
    }
}