using System;
using System.Threading.Tasks;
using Application.Visitors.VisitorOnline;
using Microsoft.AspNetCore.SignalR;

namespace WebSite.EndPoint.Hubs
{
    public class OnlineVisitorHub:Hub
    {
        private readonly IVisitorOnlineService _visitorOnlineService;

        public OnlineVisitorHub(IVisitorOnlineService visitorOnlineService)
        {
            _visitorOnlineService = visitorOnlineService;
        }

        public override Task OnConnectedAsync()
        {
            string visitorId = Context.GetHttpContext().Request.Cookies["VisitorId"];
            _visitorOnlineService.ConnectUser(visitorId);
            var count = _visitorOnlineService.GetCount();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            string visitorId = Context.GetHttpContext().Request.Cookies["VisitorId"];
            _visitorOnlineService.DisConnectUser(visitorId);
            var count = _visitorOnlineService.GetCount();
            return base.OnDisconnectedAsync(exception);
        }
    }
}
