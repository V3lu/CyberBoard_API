using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CyberBoardAPI.Entities.Configurations
{
    public class MissionConfiguration : IEntityTypeConfiguration<Mission>
    {
        public void Configure(EntityTypeBuilder<Mission> builder)
        {
            builder.HasMany(c => c.AgentsAssigned)
                .WithMany(c => c.MissionsAssigned);
        }
    }
}
