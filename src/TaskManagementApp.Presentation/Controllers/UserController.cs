
using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Application.Services;
using TaskManagementApp.Presentation.Abstraction;

namespace TaskManagementApp.Presentation.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAll());
        }
    }
}