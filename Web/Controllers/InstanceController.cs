using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI;
using UI.Services;

namespace Web.Controllers
{
    public class BaseController : Controller 
    {
 
    }
    public class InstanceController : BaseController
    {
        // GET: Instance
        [HttpPost]
        public ActionResult Index(Message msg)
        {
            //var querys = HttpContext.Request
            var features = MvcApplication.Features;
            var jmsg = features.ProcessRequest(msg);
            JsonNetResult jsonNetResult = new JsonNetResult();
            //jsonNetResult.Formatting = Formatting.Indented;
            jsonNetResult.Data = jmsg;

            return jsonNetResult;
        }
    }
}