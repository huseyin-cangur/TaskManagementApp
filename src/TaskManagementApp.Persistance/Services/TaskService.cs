

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Application.Repositories;
using TaskManagementApp.Application.Services;
using TaskManagementApp.Domain;
using TaskManagementApp.Domain.Dtos;
using TaskManagementApp.Domain.Entities;
using TaskManagementApp.Persistance.Repositories;


namespace TaskManagementApp.Persistance.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserTaskRepository _userTaskRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IUserTaskRepository userTaskRepository, IMapper mapper = null, IUnitOfWork unitOfWork = null)
        {
            _taskRepository = taskRepository;
            _userTaskRepository = userTaskRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async System.Threading.Tasks.Task AddAsync(CreateTaskDto dto)
        {
            Domain.Entities.Task task = _mapper.Map<Domain.Entities.Task>(dto);
            task.Id=Guid.NewGuid().ToString();
            task.CreatedDate=DateTime.Now;
            task.UpdatedDate=DateTime.Now;
            await _taskRepository.AddAsync(task);

            UserTask userTask = new UserTask
            {
                TaskId = task.Id,
                UserId = dto.UserId
            };
            await _userTaskRepository.AddAsync(userTask);

            _unitOfWork.SaveChangesAsync();

        }

        public async System.Threading.Tasks.Task RemoveByIdAsync(string id)
        {
            await _taskRepository.RemoveByIdAsync(id);

            var userTasks = await _userTaskRepository.GetWhere(p => p.TaskId == id).ToListAsync();
            _userTaskRepository.RemoveRange(userTasks);
             _unitOfWork.SaveChangesAsync();
        }

        public void UpdateAsync(UpdateTaskDto dto)
        {
            Domain.Entities.Task task = _mapper.Map<Domain.Entities.Task>(dto);
            _taskRepository.Update(task);
             _unitOfWork.SaveChangesAsync();

        }
    }
}