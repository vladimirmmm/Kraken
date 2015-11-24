using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Web.Hubs
{
    public class EngineHub : Hub
    {
        public static void SendText(string msg)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<EngineHub>();
            hubContext.Clients.All.sendText(msg);
        }

        public static void SendMessage(BaseModel.Message msg)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<EngineHub>();

            hubContext.Clients.All.sendMessage(msg);
        }

    
    }
}