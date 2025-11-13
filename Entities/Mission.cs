namespace CyberBoardAPI.Entities
{
    public class Mission
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Priority { get; set; }
        public List<Agent> AgentsAssigned = [];
    }
}
