using _0_Framework.Infrastructure;
using SecurityService.Application.Service.Camera.IPCamera;
using SecuritySystem.Domain.Camera;

namespace SecuritySystem.Infrastructre.Repository
{
    public class IPCameraRepository : RepositoryBase<long, IPCamera>, IIPCameraRepository
    {
        private readonly Context _context;

        public IPCameraRepository(Context context) : base(context)
        {
            _context = context;
        }

        public EditIPCamera GetDetails(long id)
        {
            return _context.IPCamera.Select(x => new EditIPCamera
            {
                HostAddress = x.HostAddress,
                Id = x.Id,
                Password = x.Password,
                StreamAddress = x.StreamAddress,
                UserName = x.UserName,
                CameraName = x.CameraName
            }).FirstOrDefault(x => x.Id == id);
        }

        public void Remove(long id)
        {
            var c = _context.IPCamera.FirstOrDefault(x => x.Id == id);
            if (c != null)
            {
                _context.Remove(c);
            }
        }

        public List<IPCameraViewModel> Search(IPCameraSearchModel command)
        {
            var query = _context.IPCamera.Select(x => new IPCameraViewModel
            {
                HostAddress = x.HostAddress,
                Id = x.Id,
                Password = x.Password,
                StreamAddress = x.StreamAddress,
                UserName = x.UserName,
                CameraName = x.CameraName
            });

            if (!string.IsNullOrWhiteSpace(command.HostAddress))
            {
                query = query.Where(x => x.HostAddress.Contains(command.HostAddress));
            }
            if (!string.IsNullOrWhiteSpace(command.CameraName))
            {
                query = query.Where(x => x.CameraName.Contains(command.CameraName));
            }

            return query.ToList();
        }
    }
}
