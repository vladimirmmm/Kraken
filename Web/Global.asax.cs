using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UI;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static Features Features = null;
       
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            var ui = new UIService();
            Features = new Features(ui);
            var folder = HttpContext.Current.Server.MapPath("~");
            System.IO.Directory.SetCurrentDirectory(folder);
            Features.LoadTaxonomy(@"C:\Users\vladimir.balacescu\AppData\Roaming\X-TreeMe\Taxonomies\www.eba.europa.eu\eu\fr\xbrl\crr\fws\corep\its-2014-05\2015-02-16\mod\corep_con.xsd");

            var z = 0;
        }
    }
}
