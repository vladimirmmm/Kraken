using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Desktop;

namespace Web.Controllers
{
    public class BaseController : Controller 
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            var exceptionstring = String.Format("{0}", filterContext.Exception);
            var requestmsg = GetMessageFromRequest();
            requestmsg.Error = exceptionstring;
            filterContext.Result = new JsonResult
            {
                Data = requestmsg,
                //Data = new { success = false, error = filterContext.Exception.ToString() },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            //ThrowJsonError(filterContext.Exception);
            //base.OnException(filterContext);
        }
        public JsonResult ThrowJsonError(Exception e)
        {
            //Utilities.Logger.Error(e.Message, e);

            Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            Response.StatusDescription = e.Message;

            return Json(new { Message = e.Message }, JsonRequestBehavior.AllowGet);
        }

        public BaseModel.Message GetMessageFromRequest() 
        {
            var msg = new BaseModel.Message();
            var querys = HttpContext.Request.QueryString;
            msg.Category = querys["msg[Category]"];
            msg.Url = querys["msg[Url]"];
            msg.Id = querys["msg[Id]"];
            msg.Data = querys["msg[Data]"];
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
            return msg;
        }
    }
    public class InstanceController : BaseController
    {
  
        // GET: Instance
        [HttpGet]
        public ActionResult Index()
        {
            //throw new Exception("testy");
            var querys = HttpContext.Request.QueryString;
            //msg[Category]=ajax&msg[Url]=Layout/Taxonomy_Validations.html&msg[Id]=6b256208-c7b7-ada8-93d6-eee130f3c0ca&msg[ContentType]=text/html&_=1447835900729
            var msg = GetMessageFromRequest();
            var features = MvcApplication.engine.Features;

            var jmsg = features.ProcessRequest(msg);
            JsonNetResult jsonNetResult = new JsonNetResult();
            //jsonNetResult.Formatting = Formatting.Indented;
            jsonNetResult.Data = jmsg;

            return jsonNetResult;
        }
        [HttpPost]
        public ActionResult Index(BaseModel.Message msg)
        {
            //var querys = HttpContext.Request
            var features = MvcApplication.engine.Features;
            var jmsg = features.ProcessRequest(msg);
            JsonNetResult jsonNetResult = new JsonNetResult();
            jmsg.Data = jmsg.Data.Replace("\r\n", "");

            //jsonNetResult.Formatting = Formatting.Indented;
            jsonNetResult.Data = jmsg;

            return jsonNetResult;
        }
    }
}