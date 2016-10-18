using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Desktop;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private Features features = MvcApplication.engine.Features;
        // GET: Home
        public ActionResult Index()
        {
            return Redirect("UI.html");


        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            var taxonmypath = form["taxonomypath"];

            var instancepath = form["instancepath"];
            var instances = instancepath.Split(new string[]{","},StringSplitOptions.RemoveEmptyEntries);
            instancepath= instances.FirstOrDefault();
            if (!String.IsNullOrEmpty(instancepath))
            {
                features.OpenInstance(instancepath);
                return Redirect("UI.html");
            }

            if (!String.IsNullOrEmpty(taxonmypath))
            {
                features.OpenTaxonomy(taxonmypath);
            }
            
            return View("UI.html");
        }
    }
}