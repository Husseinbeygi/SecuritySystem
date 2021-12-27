using _0_Framework.Application;
using SecurityService.Application.Service.Camera.IPCamera;
using SecuritySystem.Domain.Camera;

namespace SecuritySystem.Application
{
    public class IPCameraApplication : IIPCameraApplication
    {
        private readonly IIPCameraRepository _repository;

        public IPCameraApplication(IIPCameraRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateIPCamera command)
        {
            var result = new OperationResult(); 
            if (_repository.Exists(x => x.HostAddress == command.HostAddress))
            {
                return result.Failed(ApplicationMessages.RecordNotFound);
            }
            var _create = new IPCamera(command.HostAddress, command.UserName, command.Password,command.StreamAddress,command.CameraName);
            _repository.Create(_create);
            _repository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult Edit(EditIPCamera editIpc)
        {
            var result = new OperationResult();

            var _c = _repository.Get(editIpc.Id);
            if (_repository.Exists(x => x.HostAddress == editIpc.HostAddress && x.Id != editIpc.Id))
            {
                return result.Failed(ApplicationMessages.DuplicatedRecord);
            }
            _c.Edit(editIpc.HostAddress, editIpc.UserName, editIpc.Password, editIpc.StreamAddress,editIpc.CameraName);
            _repository.SaveChanges();
            return result.Succedded();

        }

        public EditIPCamera GetDetails(long id)
        {
            return _repository.GetDetails(id);
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

        public List<IPCameraViewModel> Search(IPCameraSearchModel command)
        {
            return _repository.Search(command);

        }
    }
}
