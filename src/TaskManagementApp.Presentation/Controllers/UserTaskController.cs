

using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Application.Services;
using TaskManagementApp.Domain.Dtos;
using TaskManagementApp.Presentation.Abstraction;

namespace TaskManagementApp.Presentation.Controllers
{
    public class UserTaskController:ApiController
    {
        private readonly IUserTaskService _userTaskService;

        public UserTaskController(IUserTaskService userTaskService)
        {
            _userTaskService = userTaskService;
        }

        [HttpPost]

         public async Task<ActionResult> Create(CreateUserTaskDto dto)
         {         
            return Ok( _userTaskService.AddAsync(dto));
         }

         [HttpGet]

          public async Task<ActionResult> RemoveUser(RemoveUserTaskDto dto)
         {
            _userTaskService.RemoveUser(dto);

            return Ok();
         }

    }
}