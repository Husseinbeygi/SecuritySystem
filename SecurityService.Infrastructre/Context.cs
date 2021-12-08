using Microsoft.EntityFrameworkCore;
using SecuritySystem.Domain.Client;
using SecuritySystem.Infrastructre.Maps;

namespace SecuritySystem.Infrastructre
{
    public class Context : DbContext
    {
        public DbSet<Client> Client { get; set; }
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
