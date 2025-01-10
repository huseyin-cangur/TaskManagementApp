

using TaskManagementApp.Domain.Abstraction;

namespace TaskManagementApp.Domain.Dtos
{
    public class CreateTaskDto
    {
        
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}