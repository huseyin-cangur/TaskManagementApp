

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Domain.Abstraction;

namespace TaskManagementApp.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        Task RemoveByIdAsync(string id);
        DbSet<T> Entity { get; }


    }
}