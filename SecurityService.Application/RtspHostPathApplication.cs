using _0_Framework.Application;
using SecurityService.Application.Service.RtspHostPath;
using SecuritySystem.Domain.RtspHostPathAgg;

namespace SecuritySystem.Application
{
    public class RtspHostPathApplication : IRtspHostPathApplication
    {
        private readonly IRtspHostPathRepository _repository;

        public RtspHostPathApplication(IRtspHostPathRepository repository)
        {
            _repository=repository;
        }

        public OperationResult Create(CreateRtspHostPath command)
        {
            var result = new OperationResult();
            if (_repository.Exists(x => x.Address == command.Address))
            {
                return result.Failed(ApplicationMessages.RecordNotFound);
            }
            var _create = new RtspHostPath(command.Address);
            _repository.Create(_create);
            _repository.SaveChanges();
            return result.Succedded();

        }

        public List<RtspHostPathViewModel> List()
        {
            return _repository.List();
        }

        public OperationResult Remove(long id)
        {
            var result = new OperationResult();
            var _c = _repository.Get(id);
            if (_repository.Exists(x => _c.Id == id))
            {
                _repository.Remove(id);
                _repository.SaveChanges();
                return result.Succedded();
            }
            return result.Failed(ApplicationMessages.RecordNotFound);
        }
    }
}
