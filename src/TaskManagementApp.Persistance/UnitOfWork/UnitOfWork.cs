

using TaskManagementApp.Domain;
using TaskManagementApp.Persistance.Context;

namespace TaskManagementApp.Persistance.UnitOfWork
{
    public class UnitOfWork :IUnitOfWork,IDisposable
    {
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }



        public void SaveChangesAsync()

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
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _appDbContext.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}