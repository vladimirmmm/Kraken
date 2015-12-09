using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Desktop;
using Web.Engine;
using System.Diagnostics;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static AppEngine engine = new AppEngine();
       
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            var ui = new WebUIService();
            Utilities.Logger.action = ui.Log;
            engine.Start(ui);
            var folder = HttpContext.Current.Server.MapPath("~");
            System.IO.Directory.SetCurrentDirectory(folder);
            //Features.LoadTaxonomy(@"C:\Users\vladimir.balacescu\AppData\Roaming\X-TreeMe\Taxonomies\www.eba.europa.eu\eu\fr\xbrl\crr\fws\corep\its-2014-05\2015-02-16\mod\corep_con.xsd");
            //Features.OpenInstance(@"C:\My\XBRL\Taxonomies\rtf_1.0\beispielinstanz\WIDAT.TA.1106999.20150603.092007.RTF_con.xbrl");
            //Features.LoadTaxonomy(@"C:\Users\vladimir.balacescu\AppData\Roaming\X-TreeMe\Taxonomies\www.eba.europa.eu\eu\fr\xbrl\crr\fws\corep\its-2014-05\2015-02-16\mod\corep_con.xsd");

            var z = 0;
        }
    }
}
