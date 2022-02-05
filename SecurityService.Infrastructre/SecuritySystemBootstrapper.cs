using _0_Framework.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SecurityService.Application.Service.Camera.IPCamera;
using SecurityService.Application.Service.Client;
using SecurityService.Application.Service.RtspHostPath;
using SecuritySystem.Application;
using SecuritySystem.Domain.Camera;
using SecuritySystem.Domain.ClientAgg;
using SecuritySystem.Domain.RtspHostPathAgg;
using SecuritySystem.Infrastructre.Repository;

namespace SecuritySystem.Infrastructre
{
    public static class SecuritySystemBootstrapper
    {

        public static void Configure(IServiceCollection _service, string connstring)
        {
            _service.AddTransient<IClientRepository, ClientRepository>();
            _service.AddTransient<IClientApplication, ClientApplication>();
            _service.AddTransient<IPasswordHasher, PasswordHasher>();

            _service.AddTransient<IIPCameraRepository, IPCameraRepository>();
            _service.AddTransient<IIPCameraApplication, IPCameraApplication>();


            _service.AddTransient<IRtspHostPathRepository, RtspHostPathRepository>();
            _service.AddTransient<IRtspHostPathApplication, RtspHostPathApplication>();

            _service.AddDbContext<Context>(x => x.UseSqlite(connstring));
        }
    }
}
