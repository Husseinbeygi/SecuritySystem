using _0_Framework.Infrastructure;
using SecuritySystem.Application.Dtos.Devices;
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

        public List<DeviceViewModel> Search(DeviceSearchModel command)
        {
            var query = _context.Device.Select(x => new DeviceViewModel
            {
                   CreationDate = x.CreationDate,
                   DeviceId = x.DeviceId,
                   Id = x.Id,
                   UserName = x.UserName
            });

            if (command.DeviceId != null)
            {
                query = query.Where(x => x.DeviceId == command.DeviceId);

            }

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}