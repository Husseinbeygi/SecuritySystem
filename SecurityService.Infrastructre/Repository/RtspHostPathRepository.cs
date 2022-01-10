using _0_Framework.Infrastructure;
using SecurityService.Application.Service.RtspHostPath;
using SecuritySystem.Domain.RtspHostPathAgg;

namespace SecuritySystem.Infrastructre.Repository
{
    public class RtspHostPathRepository : RepositoryBase<long, RtspHostPath>, IRtspHostPathRepository
    {
        private readonly Context _context;


        public RtspHostPathRepository(Context context) : base(context)
        {
            _context=context;

        }

        public List<RtspHostPathViewModel> List()
        {
            return _context.RtspHostPath.Select(x => new RtspHostPathViewModel
            {
                Address = x.Address,
            }).ToList();
        }

        public void Remove(long id)
        {
            var client = _context.RtspHostPath.FirstOrDefault(x => x.Id == id);
            if (client != null)
            {
                _context.Remove(client);
            }
        }
    }
}
