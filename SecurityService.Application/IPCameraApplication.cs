using SecurityService.Application.Service.Dtos.Camera.IPCamera;
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

        public void Create(CreateIPCamera command)
        {
            if (_repository.Exists(x => x.HostAddress == command.HostAddress))
            {
                return;
            }
            var _create = new IPCamera(command.HostAddress, command.UserName, command.Password,command.StreamAddress,command.CameraName);
            _repository.Create(_create);
            _repository.SaveChanges();
        }

        public void Edit(EditIPCamera editIpc)
        {
            var _c = _repository.Get(editIpc.Id);
            if (_repository.Exists(x => x.HostAddress == editIpc.HostAddress && x.Id != editIpc.Id))
            {
                return;
            }
            _c.Edit(editIpc.HostAddress, editIpc.UserName, editIpc.Password, editIpc.StreamAddress,editIpc.CameraName);
            _repository.SaveChanges();
        }

        public EditIPCamera GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public void Remove(long id)
        {
            _repository.Remove(id);
            _repository.SaveChanges();
        }

        public List<IPCameraViewModel> Search(IPCameraSearchModel command)
        {
            return _repository.Search(command);

        }
    }
}
