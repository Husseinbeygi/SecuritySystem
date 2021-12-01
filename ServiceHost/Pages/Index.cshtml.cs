using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using ServiceHost.Hubs;
using FFmpegService;
using EventService.MessageEvent;
using EventService.SubscriptionEvent;
using EventService.ConnectionEvent;

namespace ServiceHost.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHubContext<MessageHub> _hub;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly MessageInterceptorEvent _messageInterceptorEvent;
        private readonly SubscriptionInterceptorEvent _subscriptionInterceptorEvent;
        private readonly ConnectionInterceptorEvent _connectionInterceptorEvent;



        public IndexModel(ILogger<IndexModel> logger, IHubContext<MessageHub> hub, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _hub = hub;
            _webHostEnvironment = webHostEnvironment;
            _messageInterceptorEvent = MessageInterceptorEventFactory.build();
            _subscriptionInterceptorEvent = SubscriptionInterceptorEventFactory.build();
            _connectionInterceptorEvent = ConnectionInterceptorEventFactory.build();
        }

        public void OnGet()
        {
            _messageInterceptorEvent.MessageRecevied += new System.EventHandler<MessageInterceptorEventArgs>(_messageInterceptorEvent_MessageRecevied);
            _subscriptionInterceptorEvent.ClientSubscribed += new System.EventHandler<SubscriptionInterceptorEventArgs>(_subscriptionInterceptorEvent_ClientSubscribed) ;
            _connectionInterceptorEvent.ClientConnected += _connectionInterceptorEvent_ClientConnected;
        }

        private void _connectionInterceptorEvent_ClientConnected(object? sender, ConnectionInterceptorEventArgs e)
        {
            _hub.Clients.All.SendAsync("ClientConnected", e.ClientId, e.Username,e.Endpoint);
        }

        private void _subscriptionInterceptorEvent_ClientSubscribed(object? sender, SubscriptionInterceptorEventArgs e)
        {
            _hub.Clients.All.SendAsync("ClientSubscribed", e.ClientId, e.TopicFilter.Topic);
        }

        private void _messageInterceptorEvent_MessageRecevied(object source, MessageInterceptorEventArgs args)
        {
                string _rootPath = $"{_webHostEnvironment.WebRootPath}//ImageFiles";
                var mediaInfo = "rtsp://admin:123456@192.168.1.200:8554/profile0";
            if (args.PayLoad == "1")
            {
                FFmpegImage.TakeImage(mediaInfo, _rootPath);
            }
            else if (args.PayLoad == "2")
            {
                FFmpegVideo.TakeVideo(mediaInfo, _rootPath);
            }
            else
            {
                _hub.Clients.All.SendAsync("ReceiveMessage", args.ClientId, args.PayLoad);
            }
        }
    }
}