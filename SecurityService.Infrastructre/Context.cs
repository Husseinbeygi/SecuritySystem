using Microsoft.EntityFrameworkCore;
using SecuritySystem.Domain.Device;
using SecuritySystem.Infrastructre.Maps;

namespace SecuritySystem.Infrastructre
{
    public class Context : DbContext
    {
        public DbSet<Device> Device { get; set; }
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var assambly = typeof(DeviceMap).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assambly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
