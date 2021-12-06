using _0_Framework.Infrastructure;
using SecuritySystem.Domain.Device;

namespace SecuritySystem.Infrastructre.Repository
{
    public class DeviceRepository : RepositoryBase<long, Device>, IDeviceRepository
    {
        private readonly Context _context;
        public DeviceRepository(Context context) : base(context)
        {
            _context = context;
        }
    }
}