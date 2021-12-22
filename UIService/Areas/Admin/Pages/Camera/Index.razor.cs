using SecurityService.Application.Service.Dtos.Camera.IPCamera;

namespace UIService.Areas.Admin.Pages.Camera
{

    public partial class  Index
    {
        private IPCameraSearchModel searchModel = new IPCameraSearchModel();

        public Index()
        {
        }

        public void OnDeleteIPCamera(long id)
        {
            _application.Remove(id);

        }
    }
}
