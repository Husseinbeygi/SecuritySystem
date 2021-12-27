using _0_Framework.Domain;

namespace SecuritySystem.Domain.Camera
{
    public class IPCamera : EntityBase
    {
        public IPCamera(string hostAddress, string userName, string password, string streamAddress, string cameraName)
        {
            HostAddress = hostAddress;
            UserName = userName;
            Password = password;
            StreamAddress = streamAddress;
            CameraName = cameraName;
        }
        public void Edit(string hostAddress, string userName, string password, string streamAddress, string cameraName)
        {
            HostAddress = hostAddress;
            UserName = userName;
            Password = password;
            StreamAddress = streamAddress;
            CameraName = cameraName;
        }


        public string HostAddress { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string StreamAddress { get; private set; }
        public string CameraName { get; private set; }

    }
}
