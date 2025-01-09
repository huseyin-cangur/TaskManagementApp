

using System.ComponentModel.DataAnnotations.Schema;
using TaskManagementApp.Domain.Abstraction;

namespace TaskManagementApp.Domain.Entities
{
    public class UserTask:BaseEntity
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        
        [ForeignKey("Task")]
        public string TaskId { get; set; }
        public Task Task { get; set; }
    }
}