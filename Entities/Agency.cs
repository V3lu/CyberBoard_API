namespace CyberBoardAPI.Entities
{
    public class Agency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Agent> Agents { get; set; }
        public DateTime StartingDate { get; set; }
    }
}
