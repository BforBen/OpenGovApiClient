using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Caching;
using OpenGovApiClient.Models;

namespace OpenGovApiClient.Controllers
{
    public class HomeController : Controller
    {
        private MemoryCache MemCache = MemoryCache.Default;

        public HomeController()
        {
            ViewData["Organisation"] = MemCache.Get("Organisation");
        }

        [Route]
        public ActionResult Index()
        {
            return View((IEnumerable<ServiceTask>)MemCache.Get("TaskList"));
        }

        [Route("browse/{category}")]
        public ActionResult Browse(string Category)
        {
            var Tasks = ((IEnumerable<ServiceTask>)MemCache.Get("TaskList")).Where(t => t.CategorySlug == Category);

            if (Tasks == null)
            {
                return HttpNotFound();
            }

            return View(Tasks);
        }

        public ActionResult Task(string dynamicRoute)
        {
            var Task = ((IEnumerable<ServiceTask>)MemCache.Get("TaskList")).Where(t => t.Slug == dynamicRoute).SingleOrDefault();

            if (Task == null)
            {
                return HttpNotFound();
            }

            return View(Task);
        }

        [Route("About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Route("Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}