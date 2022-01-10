using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecuritySystem.Domain.RtspHostPathAgg;

namespace SecuritySystem.Infrastructre.Maps
{
    public class RtspHostPathMap : IEntityTypeConfiguration<RtspHostPath>
    {
        public void Configure(EntityTypeBuilder<RtspHostPath> builder)
        {
            builder.ToTable("RtspHostPath");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Address).HasMaxLength(30);

        }
    }
}
