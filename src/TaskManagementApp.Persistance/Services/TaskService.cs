

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Application.Repositories;
using TaskManagementApp.Application.Services;
using TaskManagementApp.Domain;
using TaskManagementApp.Domain.Dtos;
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Persistance.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserTaskRepository _userTaskRepository;
        private readonly IRoleService _roleService;


        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IUserTaskRepository userTaskRepository, IMapper mapper = null, IUnitOfWork unitOfWork = null, IRoleService roleService = null)
        {
            _taskRepository = taskRepository;
            _userTaskRepository = userTaskRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _roleService = roleService;
        }

        public async System.Threading.Tasks.Task AddAsync(CreateTaskDto dto)
        {
            Domain.Entities.Task task = _mapper.Map<Domain.Entities.Task>(dto);
            task.Id = Guid.NewGuid().ToString();
            task.CreatedDate = DateTime.Now.ToUniversalTime();
            task.UpdatedDate = DateTime.Now.ToUniversalTime();
            await _taskRepository.AddAsync(task);

            UserTask userTask = new UserTask
            {
                Id = Guid.NewGuid().ToString(),
                TaskId = task.Id,
                UserId = dto.UserId,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                UpdatedDate = DateTime.Now.ToUniversalTime(),

            };
            await _userTaskRepository.AddAsync(userTask);

            _unitOfWork.SaveChangesAsync();

        }

        public IList<Domain.Entities.Task> GetAll(string userId)
        {

            bool checkIsAdmin = _roleService.isAdmin(userId);
            
            if (checkIsAdmin)
                return _taskRepository.GetAll().ToList();


            var tasks = from userTask in _userTaskRepository.GetAll()
                        join task in _taskRepository.GetAll() on userTask.TaskId equals task.Id
                        where userTask.UserId == userId
                        select task;

            return tasks.ToList();


        }

        public async Task<Domain.Entities.Task> GetById(string id)
        {
           return await _taskRepository.GetByIdAsync(id);
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