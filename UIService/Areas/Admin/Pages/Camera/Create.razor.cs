using IPCameraClient;
using OnvifDiscovery.Models;
using SecurityService.Application.Service.Camera.IPCamera;

namespace UIService.Areas.Admin.Pages.Camera
{
    public partial class Create
    {
        public Create()
        {
        }

        public CreateIPCamera createIPcamera { get; set; } = new();
        public IEnumerable<DiscoveryDevice> cameraList { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            cameraList = new List<DiscoveryDevice>();
            cameraList = await DiscoverCamera.OnvifCamera();

        }

        public void HandleValidSubmit()
        {
            _application.Create(createIPcamera);
            navigationmanager.NavigateTo("/admin/camera");

        }

        private async Task<IEnumerable<string>> SearchAsync(string value)
        {
            if (string.IsNullOrEmpty(value))
                return cameraList.Select(u => u.Address);

            return cameraList.Where(x => x.Address.Contains(value)).Select(u => u.Address);
        }


        public async Task<IEnumerable<string>> GetRTSPAddressAsync(string value)
        {
            if (string.IsNullOrEmpty(value))
                return _rtspgenerator.returnlist();

        }

        public string GenerateRTSPUrl()
        {
            return _rtspgenerator.GenerateUrl(createIPcamera.HostAddress, createIPcamera.UserName, createIPcamera.Password, createIPcamera.StreamAddress);
        }
    }


}
