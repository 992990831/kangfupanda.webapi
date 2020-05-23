using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kangfupanda.webapi.Controllers
{
    public class HomeController : Controller
    {
        static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HomeController));
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            logger.Info("KangfuPanda Go Go Go!");

            return View();
        }
    }
}
