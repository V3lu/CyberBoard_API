namespace CyberBoardAPI.Entities
{
    public class Agent
    {
        public Guid Id { get; set; }
        public string? Rank { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? HashedPassword { get; set; }
        public int? Experience { get; set; }
        public string? ProfilePhotoPath { get; set; }
        public List<Mission> MissionsAssigned { get; set; } = new List<Mission>();
        public List<Notification> Notifications { get; set; } = new List<Notification>();
        public Guid? AgencyId { get; set; }
        public Agency? Agency { get; set; }
    }
}
