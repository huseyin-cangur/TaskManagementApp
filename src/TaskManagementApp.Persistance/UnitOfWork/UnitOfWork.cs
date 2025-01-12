

using TaskManagementApp.Domain;
using TaskManagementApp.Persistance.Context;

namespace TaskManagementApp.Persistance.UnitOfWork
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }



        public async Task SaveChangesAsync()

        {
            try
            {
                using (var transaction = _appDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        _appDbContext.SaveChanges();
                        transaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                // TODO: Hata işleme alanı
            }
        }
        
    }
}