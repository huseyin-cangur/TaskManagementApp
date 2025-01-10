 
namespace TaskManagementApp.Domain
{
    public interface IUnitOfWork
    {
         void SaveChangesAsync();
    }
}