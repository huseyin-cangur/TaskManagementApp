

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Application.Repositories;
using TaskManagementApp.Domain.Entities;
using TaskManagementApp.Persistance.Context;

namespace TaskManagementApp.Persistance.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _appDbContext;
        public DbSet<UserRole> Entity { get; set; }

        public UserRoleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            Entity = _appDbContext.Set<UserRole>();
        }


        public async System.Threading.Tasks.Task AddAsync(UserRole entity)
        {
            await Entity.AddAsync(entity);
        }

        public async System.Threading.Tasks.Task AddRangeAsync(IEnumerable<UserRole> entities)
        {
            await Entity.AddRangeAsync(entities);
        }

        public IQueryable<UserRole> GetAll()
        {
            return Entity.AsQueryable();
        }

        public async Task<UserRole> GetByIdAsync(string id)
        {
            return await Entity.FirstOrDefaultAsync(p => p.Id == id);
        }

        public IQueryable<UserRole> GetWhere(Expression<Func<UserRole, bool>> predicate)
        {
            return Entity.Where(predicate);
        }

        public async System.Threading.Tasks.Task RemoveByIdAsync(string id)
        {
            UserRole entity = await Entity.FindAsync(id);
            if (entity != null)
                Entity.Remove(entity);
        }

        public void RemoveRange(IEnumerable<UserRole> entities)
        {
            Entity.RemoveRange(entities);
        }

        public void Update(UserRole entity)
        {
            Entity.Update(entity);
        }

        public void UpdateRange(IEnumerable<UserRole> entities)
        {
            Entity.UpdateRange(entities);
        }
    }
}