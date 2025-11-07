using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CyberBoardAPI.Entities.Configurations
{
    public class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.HasOne(c => c.Agency)
                .WithMany(c => c.Agents)
                .HasForeignKey(c => c.AgencyId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
