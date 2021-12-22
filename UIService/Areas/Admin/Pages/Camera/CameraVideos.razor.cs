using SecurityService.Application.Service.Dtos.Camera.IPCamera;

namespace UIService.Areas.Admin.Pages.Camera
{
    public partial class CameraVideos
    {
        public EditIPCamera cam { get; set; } = new();
        protected override void OnInitialized()
        {
            base.OnInitialized();
            cam = _camApplication.GetDetails(4);

        }


    }
}
