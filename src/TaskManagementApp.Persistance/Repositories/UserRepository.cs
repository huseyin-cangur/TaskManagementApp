
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Application.Repositories;
using TaskManagementApp.Domain.Entities;
using TaskManagementApp.Persistance.Context;

namespace TaskManagementApp.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
         public DbSet<User> Entity { get; set; }

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            Entity = _appDbContext.Set<User>();
        }

       
        public async System.Threading.Tasks.Task AddAsync(User entity)
        {
             await Entity.AddAsync(entity);
        }

        public async System.Threading.Tasks.Task AddRangeAsync(IEnumerable<User> entities)
        {
              await Entity.AddRangeAsync(entities);
        }

        public IQueryable<User> GetAll()
        {
             return Entity.AsQueryable();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await Entity.FirstOrDefaultAsync(p=>p.Id == id);
        }

        public IQueryable<User> GetWhere(Expression<Func<User, bool>> predicate)
        {
            return Entity.Where(predicate);
        }

        public async System.Threading.Tasks.Task RemoveByIdAsync(string id)
        {
            User entity = await Entity.FindAsync(id);
            if (entity != null)
                Entity.Remove(entity);;
        }

        public void RemoveRange(IEnumerable<User> entities)
        {
             Entity.RemoveRange(entities);
        }

        public void Update(User entity)
        {
             Entity.Update(entity);
        }

        public void UpdateRange(IEnumerable<User> entities)
        {
            Entity.UpdateRange(entities);
        }
    }
}