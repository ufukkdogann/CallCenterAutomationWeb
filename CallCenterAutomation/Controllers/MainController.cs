using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CallCenterAutomation.Models.Entity;
using CallCenterAutomation.ViewModels;

namespace CallCenterAutomation.Controllers

{
    //[Authorize]
    [Authorize]
    public class MainController : Controller
    {
        CallCenterAutomationEntities1 db = new CallCenterAutomationEntities1();
        // GET: Main
        [Authorize(Roles = "Ana Sayfa-True")]
        public ActionResult Index()
        {
            return View();
        }
    }
}