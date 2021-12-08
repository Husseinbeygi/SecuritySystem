using System.Threading;
using System.Threading.Tasks;

namespace MqttService
{
    public interface IBootstrapper
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}