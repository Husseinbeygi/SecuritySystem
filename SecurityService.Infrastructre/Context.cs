using Microsoft.EntityFrameworkCore;
using SecuritySystem.Domain.Camera;
using SecuritySystem.Domain.ClientAgg;
using SecuritySystem.Domain.RtspHostPathAgg;
using SecuritySystem.Infrastructre.Maps;

namespace SecuritySystem.Infrastructre
{
    public class Context : DbContext
    {
        public DbSet<Client> Client { get; set; }
        public DbSet<IPCamera> IPCamera { get; set; }
        public DbSet<RtspHostPath> RtspHostPath { get; set; }
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var assambly = typeof(ClientMap).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assambly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
