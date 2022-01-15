using SecurityService.Application.Service.Camera.IPCamera;

namespace UIService.Areas.Admin.Pages.Camera
{
    public partial class CameraVideos
    {
        public EditIPCamera cam { get; set; } = new();
        public EditIPCamera cam1 { get; set; } = new();

        public string fileAddress { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            cam = _camApplication.GetDetails(7);
            cam1 = _camApplication.GetDetails(12);

        }
    }
}
