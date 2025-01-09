

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Application.Services;
using TaskManagementApp.Domain.Dtos;
using TaskManagementApp.Domain.Entities;
using TaskManagementApp.Persistance.Repositories;


namespace TaskManagementApp.Persistance.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskRepository _taskRepository;
        private readonly UserTaskRepository _userTaskRepository;

        private readonly IMapper _mapper;

        public TaskService(TaskRepository taskRepository, UserTaskRepository userTaskRepository, IMapper mapper = null)
        {
            _taskRepository = taskRepository;
            _userTaskRepository = userTaskRepository;
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task AddAsync(CreateTaskDto dto)
        {
            Domain.Entities.Task task = _mapper.Map<Domain.Entities.Task>(dto);
            await _taskRepository.AddAsync(task);

            UserTask userTask = new UserTask
            {
                TaskId = dto.TaskId,
                UserId = dto.UserId
            };
            await _userTaskRepository.AddAsync(userTask);

        }

        public async System.Threading.Tasks.Task RemoveByIdAsync(string id)
        {
            await _taskRepository.RemoveByIdAsync(id);

            var userTasks = await _userTaskRepository.GetWhere(p => p.TaskId == id).ToListAsync();
            _userTaskRepository.RemoveRange(userTasks);
        }

        public void UpdateAsync(UpdateTaskDto dto)
        {
            Domain.Entities.Task task = _mapper.Map<Domain.Entities.Task>(dto);
            _taskRepository.Update(task);

        }
    }
}