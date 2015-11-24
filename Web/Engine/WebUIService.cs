using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI;
using Web.Hubs;

namespace Web.Engine
{
    public class WebUIService:UIService
    {
        public WebUIService()
        {
            Log = (string s) => {
                EngineHub.SendText(s);
            };
            ToUI = (BaseModel.Message m) =>
            {
                EngineHub.SendMessage(m);
            };
        }

    }
}