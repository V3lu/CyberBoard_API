using Microsoft.EntityFrameworkCore;

namespace CyberBoardAPI.Entities
{
    public class CyberBoardDBContext : DbContext
    {
        public CyberBoardDBContext() { }
        public CyberBoardDBContext(DbContextOptions<CyberBoardDBContext> options) : base(options)
        {

        }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);

        }
    }
}
