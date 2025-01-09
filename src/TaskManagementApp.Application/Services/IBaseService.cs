

using System.Linq.Expressions;
using TaskManagementApp.Domain.Abstraction;

namespace TaskManagementApp.Application.Services
{
    public interface IBaseService<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        Task RemoveByIdAsync(string id);
    }
}