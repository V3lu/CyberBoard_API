namespace CyberBoardAPI.Entities
{
    public class Agency
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<Agent> Agents { get; set; } = new List<Agent>();
        public DateTime? StartingDate { get; set; }
    }
}
