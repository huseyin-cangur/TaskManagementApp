

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
        private readonly IUserRepository _userRepository;


        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IUserTaskRepository userTaskRepository, IMapper mapper = null, IUnitOfWork unitOfWork = null, IRoleService roleService = null, IUserRepository userRepository = null)
        {
            _taskRepository = taskRepository;
            _userTaskRepository = userTaskRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _roleService = roleService;
            _userRepository = userRepository;
        }

        public async System.Threading.Tasks.Task<CreateTaskDto> AddAsync(CreateTaskDto dto)
        {
            Domain.Entities.Task task = _mapper.Map<Domain.Entities.Task>(dto);
            task.Id = Guid.NewGuid().ToString();
            task.CreatedDate = DateTime.Now.ToUniversalTime();
            task.UpdatedDate = DateTime.Now.ToUniversalTime();
            await _taskRepository.AddAsync(task);


            foreach (var userId in dto.UserIds)
            {
                UserTask userTask = new UserTask
                {
                    Id = Guid.NewGuid().ToString(),
                    TaskId = task.Id,
                    UserId = userId,
                    CreatedDate = DateTime.Now.ToUniversalTime(),
                    UpdatedDate = DateTime.Now.ToUniversalTime(),

                };
                await _userTaskRepository.AddAsync(userTask);
            }


            _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CreateTaskDto>(task);

        }

        public async Task<IList<TaskDto>> GetAll(string userId)
        {
            List<TaskDto> taskDtos = new List<TaskDto>();
            bool checkIsAdmin = _roleService.isAdmin(userId);

            if (checkIsAdmin)
            {
                var adminTasks = _taskRepository.GetAll().ToList();

                foreach (var task in adminTasks)
                {
                    List<string> userIds = _userTaskRepository.GetWhere(p => p.TaskId == task.Id)
                    .Select(p => p.UserId)
                    .ToList();

                    TaskDto data = _mapper.Map<TaskDto>(task);
                    data.UserNames = _userRepository.GetWhere(p => userIds.Contains(p.Id)).Select(p => p.FullName).ToList();
                    taskDtos.Add(data);

                }

            }
            else
            {
               


                var tasks = (from userTask in _userTaskRepository.GetAll()
                            join task in _taskRepository.GetAll() on userTask.TaskId equals task.Id
                            where userTask.UserId == userId
                            select task).Distinct().ToList();

                foreach (var task in tasks)
                {
                    List<string> userIds = _userTaskRepository.GetWhere(p => p.TaskId == task.Id)
                    .Select(p => p.UserId)
                    .ToList();

                    TaskDto data = _mapper.Map<TaskDto>(task);
                    data.UserNames = _userRepository.GetWhere(p => userIds.Contains(p.Id)).Select(p => p.FullName).ToList();
                    taskDtos.Add(data);

                }
            }



            return taskDtos;


        }

        public async Task<TaskDto> GetById(string id)
        {
            TaskDto dto = _mapper.Map<TaskDto>(await _taskRepository.GetByIdAsync(id));

            List<string> userIds = await _userTaskRepository.GetWhere(p => p.TaskId == id).Select(p => p.UserId).ToListAsync();

            dto.UserIds = userIds;

            return dto;
        }

        public async System.Threading.Tasks.Task<bool> RemoveByIdAsync(string id)
        {
            await _taskRepository.RemoveByIdAsync(id);

            var userTasks = await _userTaskRepository.GetWhere(p => p.TaskId == id).ToListAsync();
            _userTaskRepository.RemoveRange(userTasks);
            _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<UpdateTaskDto> UpdateAsync(UpdateTaskDto dto)
        {
            Domain.Entities.Task task = _mapper.Map<Domain.Entities.Task>(dto);
            _taskRepository.Update(task);

            var userTasks = await _userTaskRepository.GetWhere(p => p.TaskId == dto.Id).ToListAsync();

            _userTaskRepository.RemoveRange(userTasks);

            foreach (var userId in dto.UserIds)
            {
                UserTask userTask = new UserTask
                {
                    Id = Guid.NewGuid().ToString(),
                    TaskId = task.Id,
                    UserId = userId,
                    CreatedDate = DateTime.Now.ToUniversalTime(),
                    UpdatedDate = DateTime.Now.ToUniversalTime(),

                };
                await _userTaskRepository.AddAsync(userTask);
            }


            _unitOfWork.SaveChangesAsync();

            UpdateTaskDto updateTaskdto = _mapper.Map<UpdateTaskDto>(task);

            return updateTaskdto;
        }
    }
}