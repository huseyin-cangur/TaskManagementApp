

using System.ComponentModel.DataAnnotations.Schema;
using TaskManagementApp.Domain.Abstraction;

namespace TaskManagementApp.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        [ForeignKey("Role")]
        public string RoleId { get; set; }
        public Role Role { get; set; } 
        
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }



    }
}