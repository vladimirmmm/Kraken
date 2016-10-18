using Engine.Services;
using LogicalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Engine
{
    public class AppEngine
    {
        public Features Features = new Features();
        public DataService DataService = null;

        public void Start(UIService ui)
        {
            DataService = new Services.DataService(this);
            Features.DataService = DataService;
            Features.Start(ui);
            
        }
     
    }
}
