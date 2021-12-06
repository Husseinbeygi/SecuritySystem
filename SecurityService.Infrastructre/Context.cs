using Microsoft.EntityFrameworkCore;
using SecuritySystem.Domain.Device;

namespace SecuritySystem.Infrastructre
{
    public class Context : DbContext
    {
        public DbSet<Device> Device { get; set; }
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
    }
}
