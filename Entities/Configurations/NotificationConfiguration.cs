using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CyberBoardAPI.Entities.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasOne(C => C.TargetAgent)
                .WithMany(c => c.Notifications)
                .HasForeignKey(c => c.TargetAgentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
