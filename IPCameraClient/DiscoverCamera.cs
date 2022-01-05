using OnvifDiscovery;
using OnvifDiscovery.Models;

namespace IPCameraClient
{
    public class DiscoverCamera
    {
        public static async Task<IEnumerable<DiscoveryDevice>> OnvifCamera()
        {
            var onvifDiscovery = new Discovery();
            var onvifDevices = await onvifDiscovery.Discover(1);
            return onvifDevices;
        }

   }

}
