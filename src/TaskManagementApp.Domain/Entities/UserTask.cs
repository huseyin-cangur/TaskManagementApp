

using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementApp.Domain.Entities
{
    public class UserTask
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        
        [ForeignKey("Task")]
        public string TaskId { get; set; }
        public Task Task { get; set; }
    }
}