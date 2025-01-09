

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Application.Repositories;
using TaskManagementApp.Persistance.Context;

namespace TaskManagementApp.Persistance.Repositories
{
    public class TaskRepository : IRepository<Domain.Entities.Task>
    {
        private readonly AppDbContext _appDbContext;
        public DbSet<Domain.Entities.Task> Entity { get; set; }

        public TaskRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Domain.Entities.Task entity)
        {
            await Entity.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<Domain.Entities.Task> entities)
        {
            await Entity.AddRangeAsync(entities);
        }

        public IQueryable<Domain.Entities.Task> GetAll()
        {
             return Entity.AsQueryable();
        }

        public async Task<Domain.Entities.Task> GetByIdAsync(string id)
        {
            return await Entity.FirstOrDefaultAsync(p=>p.Id == id);
        }

        public IQueryable<Domain.Entities.Task> GetWhere(Expression<Func<Domain.Entities.Task, bool>> predicate)
        {
            return Entity.Where(predicate);
        }

        public async Task RemoveByIdAsync(string id)
        {
            Domain.Entities.Task entity = await Entity.FindAsync(id);
            if (entity != null)
                Entity.Remove(entity);
        }

        public void Update(Domain.Entities.Task entity)
        {
             Entity.Update(entity);
        }

        public void UpdateRange(IEnumerable<Domain.Entities.Task> entities)
        {
              Entity.UpdateRange(entities);
        }

        public void RemoveRange(IEnumerable<Domain.Entities.Task> entities)
        {
            Entity.RemoveRange(entities);
        }
    }
}