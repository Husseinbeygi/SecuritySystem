using Microsoft.EntityFrameworkCore;

namespace SecuritySystem.Infrastructre
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
    }
}
