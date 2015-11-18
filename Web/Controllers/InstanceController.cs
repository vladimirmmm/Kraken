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
        [HttpGet]
        public ActionResult Index()
        {
            var querys = HttpContext.Request.QueryString;
            //msg[Category]=ajax&msg[Url]=Layout/Taxonomy_Validations.html&msg[Id]=6b256208-c7b7-ada8-93d6-eee130f3c0ca&msg[ContentType]=text/html&_=1447835900729
            var msg = new Message();
            msg.Category = querys["msg[Category]"];
            msg.Url = querys["msg[Url]"];
            msg.Id = querys["msg[Id]"];
            msg.ContentType = querys["msg[ContentType]"];
            var parameterkeys = querys.AllKeys.Where(i => i.Contains("msg[Parameters]")).ToList();
            msg.Parameters = new Dictionary<string, string>();
            foreach (var parameterkey in parameterkeys) 
            {
                var key = Utilities.Strings.TextBetween(parameterkey, "msg[Parameters][", "]");
                if (!String.IsNullOrEmpty(key))
                {
                    var value = querys[parameterkey];
                    msg.Parameters.Add(key, value);
                }
            }
            var features = MvcApplication.Features;
            var jmsg = features.ProcessRequest(msg);
            JsonNetResult jsonNetResult = new JsonNetResult();
            //jsonNetResult.Formatting = Formatting.Indented;
            jsonNetResult.Data = jmsg;

            return jsonNetResult;
        }
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