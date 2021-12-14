using FFmpegService;
using OnvifDiscovery.Models;
using SecurityService.Application.Service.Dtos.Camera.IPCamera;

namespace UIService.Areas.Admin.Pages.Camera
{
    public partial class Create
    {
        public Create()
        {
        }

        public CreateIPCamera createIPcamera { get; set; } = new();
        public IEnumerable<DiscoveryDevice> cameraList { get; set; }
        private bool loader = false;
    
        protected override async Task OnParametersSetAsync()
        {           
            loader = true;
            cameraList = new List<DiscoveryDevice>();
            cameraList = await DiscoverCamera.OnvifCamera();  
            loader = false;

        }

        public void HandleValidSubmit()
        {
            _application.Create(createIPcamera);
        }

        private async Task<IEnumerable<string>> Search(string value)
        {
            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return cameraList.Select( u => u.Address);

            return cameraList.Where(x => x.Address.Contains(value)).Select( u=> u.Address);
        }
    }


}
