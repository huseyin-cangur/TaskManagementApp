

using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Application.Services;
using TaskManagementApp.Domain.Dtos;
using TaskManagementApp.Presentation.Abstraction;

namespace TaskManagementApp.Presentation.Controllers
{
    public class TaskController : ApiController
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public IActionResult Create(CreateTaskDto dto)
        {
           
            return Ok(_taskService.AddAsync(dto));
        }

        [HttpGet]
        public IActionResult GetAll(string userId)
        {
        
            return Ok( _taskService.GetAll(userId));
        }
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
        
            return Ok( await _taskService.GetById(id));
        }

    }
}