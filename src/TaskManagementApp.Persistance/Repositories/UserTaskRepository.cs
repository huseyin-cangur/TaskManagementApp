

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Application.Repositories;
using TaskManagementApp.Domain.Entities;
using TaskManagementApp.Persistance.Context;

namespace TaskManagementApp.Persistance.Repositories
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly AppDbContext _appDbContext;
        public DbSet<UserTask> Entity { get; set; }
        public UserTaskRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            Entity = _appDbContext.Set<UserTask>();

        }



        public async System.Threading.Tasks.Task AddAsync(UserTask entity)
        {
            await Entity.AddAsync(entity);
        }

        public async System.Threading.Tasks.Task AddRangeAsync(IEnumerable<UserTask> entities)
        {
            await Entity.AddRangeAsync(entities);
        }

        public IQueryable<UserTask> GetAll()
        {
            return Entity.AsQueryable();
        }

        public async Task<UserTask> GetByIdAsync(string id)
        {
            return await Entity.FirstOrDefaultAsync(p => p.Id == id);
        }

        public IQueryable<UserTask> GetWhere(Expression<Func<UserTask, bool>> predicate)
        {
            return Entity.Where(predicate);
        }

        public async System.Threading.Tasks.Task RemoveByIdAsync(string id)
        {
            UserTask entity = await Entity.FindAsync(id);
            if (entity != null)
                Entity.Remove(entity);
        }

        public void RemoveRange(IEnumerable<UserTask> entities)
        {
            Entity.RemoveRange(entities);
        }

        public void Update(UserTask entity)
        {
            Entity.Update(entity);
        }

        public void UpdateRange(IEnumerable<UserTask> entities)
        {
            Entity.UpdateRange(entities);
        }
    }
}