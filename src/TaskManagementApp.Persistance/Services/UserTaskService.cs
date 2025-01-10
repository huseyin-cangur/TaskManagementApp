

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Application.Repositories;
using TaskManagementApp.Application.Services;
using TaskManagementApp.Domain;
using TaskManagementApp.Domain.Dtos;
using TaskManagementApp.Domain.Entities;


namespace TaskManagementApp.Persistance.Services
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IUserTaskRepository _userTaskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserTaskService(IUserTaskRepository userTaskRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userTaskRepository = userTaskRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async System.Threading.Tasks.Task AddAsync(CreateUserTaskDto dto)
        {
            UserTask userTask = _mapper.Map<UserTask>(dto);
            userTask.Id = Guid.NewGuid().ToString();
            userTask.CreatedDate = DateTime.Now.ToUniversalTime();
            userTask.UpdatedDate = DateTime.Now.ToUniversalTime();
            await _userTaskRepository.AddAsync(userTask);

            _unitOfWork.SaveChangesAsync();
        }

        public void RemoveUser(RemoveUserTaskDto dto)
        {
            UserTask task = _userTaskRepository.GetWhere(p => p.UserId == dto.UserId && p.TaskId == dto.TaskId).FirstOrDefault();
            if (task != null)
                _userTaskRepository.RemoveByIdAsync(task.Id);

            _unitOfWork.SaveChangesAsync();
        }
    }
}