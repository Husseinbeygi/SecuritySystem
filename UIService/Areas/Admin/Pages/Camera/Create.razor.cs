using IPCameraClient;
using MudBlazor;
using OnvifDiscovery.Models;
using OnvifService;
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
        public bool isVisible = false;
        protected override async Task OnParametersSetAsync()
        {
            cameraList = new List<DiscoveryDevice>();
            cameraList = await DiscoverCamera.OnvifCamera();
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;

        }
        public async Task FindRtspAddress()
        {
            if (IsCameraValid())
            {
                isVisible = true;
                var rtspList = _rtspgenerator.returnlist();
                string rtspAddress = "rtsp://" + createIPcamera.UserName + ":"+ createIPcamera.Password + "@" + createIPcamera.HostAddress + ":8554";
                foreach (var item in rtspList)
                {
                    var address = rtspAddress + item.Address;
                    var res = await ValidateRtspAddress.IsConnectionValidAsync(address);
                    if (res == 1)
                    {
                        createIPcamera.StreamAddress = item.Address;
                        Snackbar.Add("دوربین پیدا شد", Severity.Success);
                        break;
                    }
                    else if (res == 2)
                    {
                        Snackbar.Add("عملیات توسط کاربر لغو شد", Severity.Warning);
                        break;
                    }
                    else if (res == 3)
                    {
                        Snackbar.Add("نام کاربری و رمزعبور اشتباه می باشد", Severity.Error);
                        break;

                    }
                }
                if (string.IsNullOrWhiteSpace(createIPcamera.StreamAddress))
                {
                    Snackbar.Add("دوربین را پیدا نکردم.لطفا آدرس را دستی ثبت نمایید ", Severity.Error);
                }
                isVisible = false;
            }
        }

        private bool IsCameraValid()
        {
            return createIPcamera.HostAddress != null && createIPcamera.UserName != null && createIPcamera.Password != null;
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


        public string GenerateRTSPUrl()
        {
            return _rtspgenerator.GenerateUrl(createIPcamera.HostAddress, createIPcamera.UserName, createIPcamera.Password, createIPcamera.StreamAddress);
        }
    }


}
