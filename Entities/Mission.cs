namespace CyberBoardAPI.Entities
{
    public class Mission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Priority { get; set; }
        public int AgentId { get; set; }
        public Agent Agent { get; set; }
    }
}
