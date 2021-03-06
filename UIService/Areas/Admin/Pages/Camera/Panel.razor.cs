using Microsoft.AspNetCore.Components;
using SecurityService.Application.Service.Camera.IPCamera;

namespace UIService.Areas.Admin.Pages.Camera
{
    public partial class Panel
    {

        public Panel() { }

        [Parameter]
        public long id { get; set; }
        public EditIPCamera cam { get; set; } = new();

        private string fileAddress;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            cam = _camApplication.GetDetails(id);
            fileAddress = "/admin/camera/gallery/" + cam.HostAddress;

        }

    }
}
