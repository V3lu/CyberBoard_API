namespace CyberBoardAPI.Entities
{
    public class Agent
    {
        public int Id { get; set; }
        public string Rank { get; set; }
        public string Name { get; set; }
        public string HashedPassword { get; set; }
        public int Experience { get; set; }
        public string? ProfilePhotoPath { get; set; }
        public List<Mission>? MissionsAssigned { get; set; }
        public List<Notification>? Notifications { get; set; }
        public int AgencyId { get; set; }
        public Agency Agency { get; set; }
    }
}
