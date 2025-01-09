using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementApp.Domain
{
    public interface IUnitOfWork
    {
         void SaveChangesAsync();
    }
}