namespace CyberBoardAPI.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int TargetAgentId { get; set; }
        public Agent TargetAgent { get; set; }
    }
}
