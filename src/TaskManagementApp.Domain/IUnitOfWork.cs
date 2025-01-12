 
namespace TaskManagementApp.Domain
{
    public interface IUnitOfWork
    {
         Task SaveChangesAsync();
    }
}