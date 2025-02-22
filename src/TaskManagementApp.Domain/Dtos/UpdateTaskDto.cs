
namespace TaskManagementApp.Domain.Dtos
{
    public class UpdateTaskDto
    {
        public string Id { get; set;}
        public List<string> UserIds { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}