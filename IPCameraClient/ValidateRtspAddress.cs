using RtspClientCore;
using RtspClientCore.Rtsp;
using System.Security.Authentication;

namespace IPCameraClient
{
    public class ValidateRtspAddress
    {
        public static async Task<byte> IsConnectionValidAsync(string RtspUrl)
        {
            var serverUri = new Uri(RtspUrl);
            var connectionParameters = new ConnectionParameters(serverUri);
            return await ConnectToCamera(connectionParameters);

        }

        private static async Task<byte> ConnectToCamera(ConnectionParameters connectionParameters)
        {
           var cancellationTokenSource = new CancellationTokenSource();
           return await ConnectAsync(connectionParameters, cancellationTokenSource.Token);
        }
        private static async Task<byte> ConnectAsync(ConnectionParameters connectionParameters, CancellationToken token)
        {
            try
            {
                var delay = TimeSpan.FromSeconds(5);

                using (var rtspClient = new RtspClient(connectionParameters))
                {
                    try
                    {
                        Console.WriteLine("Connecting...");
                        await rtspClient.ConnectAsync(token);
                        return 1;
                    }
                    catch (OperationCanceledException)
                    {
                        return 2;

                    }
                    catch(InvalidCredentialException e)
                    {
                        Console.WriteLine(e.ToString());
                        return 3;
                    }
                    catch (RtspClientException e)
                    {
                        Console.WriteLine(e.ToString());
                        await Task.Delay(delay, token);
                        return 4;
                    }
                    
                }
            }
            catch (OperationCanceledException)
            {
                return 2; 
            }
        }

    }
}
