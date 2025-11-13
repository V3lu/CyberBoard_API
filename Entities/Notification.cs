namespace CyberBoardAPI.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public Guid? TargetAgentId { get; set; }
        public Agent? TargetAgent { get; set; }
    }
}
