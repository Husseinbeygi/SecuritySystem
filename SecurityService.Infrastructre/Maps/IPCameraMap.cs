using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecuritySystem.Domain.Camera;

namespace SecuritySystem.Infrastructre.Maps
{
    public class IPCameraMap : IEntityTypeConfiguration<IPCamera>
    {
        public void Configure(EntityTypeBuilder<IPCamera> builder)
        {
            builder.ToTable("IPCameras");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.HostAddress).HasMaxLength(30);
            builder.Property(x => x.UserName).HasMaxLength(100);
            builder.Property(x => x.Password).HasMaxLength(100);
            builder.Property(x => x.StreamAddress).HasMaxLength(1000);
            builder.Property(x => x.CameraName).HasMaxLength(100);

        }
    }
}
