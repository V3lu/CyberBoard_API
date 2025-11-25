using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CyberBoardAPI.Entities
{
    public class Mission
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Priority { get; set; }
        public Date? creationDate { get; set; }
        public string? description { get; set; }
        public List<string>? tags { get; set; }
        public List<string>? comments { get; set; }
        public List<IFormFile>? attachments = [];
        public List<Agent> AgentsAssigned = [];
    }
}
