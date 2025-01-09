
using TaskManagementApp.Domain.Abstraction;

namespace TaskManagementApp.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string IdentityNumber { get; set; }
    }
}