using _0_Framework.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SecurityService.Application.Service.Dtos.Client;
using SecuritySystem.Application;
using SecuritySystem.Domain.Client;
using SecuritySystem.Infrastructre.Repository;

namespace SecuritySystem.Infrastructre
{
    public static class  SecuritySystemBootstrapper
    {

        public static void Configure(IServiceCollection _service, string connstring)
        {
            _service.AddTransient<IClientRepository, ClientRepository>();
            _service.AddTransient<IClientApplication, ClientApplication>();
            _service.AddTransient<IPasswordHasher, PasswordHasher>();


            _service.AddDbContext<Context>(x => x.UseSqlite(connstring));
        }
    }
}
