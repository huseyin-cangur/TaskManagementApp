
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Application.Repositories;
using TaskManagementApp.Domain.Entities;
using TaskManagementApp.Persistance.Context;

namespace TaskManagementApp.Persistance.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _appDbContext;
        public DbSet<Role> Entity { get; set; }

        public RoleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            Entity = _appDbContext.Set<Role>();
        }


        public async System.Threading.Tasks.Task AddAsync(Role entity)
        {
            await Entity.AddAsync(entity);
        }

        public async System.Threading.Tasks.Task AddRangeAsync(IEnumerable<Role> entities)
        {
            await Entity.AddRangeAsync(entities);
        }

        public IQueryable<Role> GetAll()
        {
            return Entity.AsQueryable();
        }

        public async Task<Role> GetByIdAsync(string id)
        {
            return await Entity.FirstOrDefaultAsync(p => p.Id == id);
        }

        public IQueryable<Role> GetWhere(Expression<Func<Role, bool>> predicate)
        {
            return Entity.Where(predicate);
        }

        public async System.Threading.Tasks.Task RemoveByIdAsync(string id)
        {
            Role entity = await Entity.FindAsync(id);
            if (entity != null)
                Entity.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Role> entities)
        {
            Entity.RemoveRange(entities);
        }

        public void Update(Role entity)
        {
            Entity.Update(entity);
        }

        public void UpdateRange(IEnumerable<Role> entities)
        {
            Entity.UpdateRange(entities);
        }
    }
}