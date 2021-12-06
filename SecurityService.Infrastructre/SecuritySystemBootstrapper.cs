using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SecurityService.Application.Service.Dtos.Devices;
using SecuritySystem.Application;
using SecuritySystem.Domain.Device;
using SecuritySystem.Infrastructre.Repository;

namespace SecuritySystem.Infrastructre
{
    public static class  SecuritySystemBootstrapper
    {

        public static void Configure(IServiceCollection _service, string connstring)
        {
            _service.AddTransient<IDeviceRepository, DeviceRepository>();
            _service.AddTransient<IDeviceApplication, DeviceApplication>();



            _service.AddDbContext<Context>(x => x.UseSqlite(connstring));
        }
    }
}
