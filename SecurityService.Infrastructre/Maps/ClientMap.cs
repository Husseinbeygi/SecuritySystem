using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecuritySystem.Domain.ClientAgg;

namespace SecuritySystem.Infrastructre.Maps
{
    internal class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserName).HasMaxLength(100);
            builder.Property(x => x.Password).HasMaxLength(300);
            builder.Property(x => x.ClientId).HasMaxLength(100);


            builder.OwnsMany(x => x.ClientTopics, builder =>
            {
                builder.ToTable("ClientTopics");
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Topic).HasMaxLength(128).IsRequired();
                builder.Property(x => x.ClientId).HasMaxLength(100).IsRequired();
                builder.Property(x => x.Caption).HasMaxLength(128).IsRequired();
                builder.WithOwner(x => x.Client).HasForeignKey(x => x.ClientId).HasPrincipalKey(x => x.ClientId);
            });
        }
    }
}
