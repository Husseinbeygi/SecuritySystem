using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecuritySystem.Domain.Device;

namespace SecuritySystem.Infrastructre.Maps
{
    internal class DeviceMap : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Devices");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserName).HasMaxLength(100);
            builder.Property(x => x.Password).HasMaxLength(300);
            builder.Property(x => x.ClientId).HasMaxLength(100);

        }
    }
}
