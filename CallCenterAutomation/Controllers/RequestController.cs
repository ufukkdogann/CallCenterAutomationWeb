using CallCenterAutomation.Models.Entity;
using CallCenterAutomation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CallCenterAutomation.Controllers
{
    public class RequestController : Controller
    {
        CallCenterAutomationEntities1 db = new CallCenterAutomationEntities1();
        [Authorize(Roles = "Arıza/Talep İşlemleri-True")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Arıza/Talep Ekle-True")]
        [HttpGet]
        public ActionResult AddRequest()
        {
            var model = new RequestForViewModel()
            {
                requestHeaderList = db.REQUESTHEADERS.Where(x => x.isActive == true).ToList(),
            };
            return View("AddRequest", model);
        }
        [Authorize(Roles = "Arıza/Talep Ekle-True")]
        [HttpPost]
        public ActionResult AddRequest(REQUEST request)
        {
            if (request.Anydesk.Length > 1 || request.Anydesk.Length > 12 || request.Anydesk.Length < 9 || request.Anydesk == "0")
            {
                ViewBag.AnydeskLengthMessage = "En az 9, en fazla 12 karakter olabilir.";
                var model = new RequestForViewModel()
                {
                    requestHeaderList = db.REQUESTHEADERS.Where(x => x.isActive == true).ToList(),
                };
                return View("AddRequest", model);
            }
            else
            {
                var model = db.REQUESTS.OrderByDescending(x => x.AddedTime).ToList();
                request.UserID = (int)Session["LoginUserID"];
                request.AddedTime = DateTime.Now;
                request.RequestStatusID = 1; //Default olarak açık kayıt işler. => Database tarafında 1: Açık Kayıt
                db.REQUESTS.Add(request);
                db.SaveChanges();
                return RedirectToAction("MyRequests", model);
            }

        }
        [Authorize(Roles = "Benim Taleplerim/Arızalarım-True")]
        [HttpGet]
        public ActionResult MyRequests()
        {
            var model = db.REQUESTS.OrderByDescending(x => x.AddedTime).ToList();
            return View("MyRequests", model);
        }
        [Authorize(Roles = "Tüm Talepler/Arıza Kayıtları-True")]
        [HttpGet]
        public ActionResult AllRequests()
        {
            var model = db.REQUESTS.OrderByDescending(x => x.AddedTime).ToList();
            return View("AllRequests", model);
        }
        [Authorize(Roles = "Talep/Arıza Kaydı Detayları-True")]
        public ActionResult RequestDetail(REQUEST request)
        {
            TempData["RequestID"] = request.ID;
            var model = new RequestForViewModel()
            {
                request = db.REQUESTS.Find(request.ID),
                requestList = db.REQUESTS.ToList(),
                requestStatusList = db.REQUESTSTATUSES.ToList(),
                requestHeaderList = db.REQUESTHEADERS.ToList(),
                userList = db.USERS.ToList(),
                requestProcessesList = db.REQUESTPROCESSES.Where(x => x.RequestID == request.ID).OrderByDescending(x => x.ProcessTime).ToList()
            };
            if (model == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View("RequestOperations", model);
            }
        }
        [Authorize(Roles = "Arıza/Talep İşlemleri-True")]
        [HttpGet]
        public ActionResult RequestOperations()
        {
            var model = new RequestForViewModel()
            {
                requestList = db.REQUESTS.ToList(),
                requestStatusList = db.REQUESTSTATUSES.ToList(),
                requestHeaderList = db.REQUESTHEADERS.ToList(),
                userList = db.USERS.ToList(),
                requestProcessesList = db.REQUESTPROCESSES.ToList()
            };
            return View("RequestOperations", model);
        }
        [Authorize(Roles = "Arıza/Talep İşlemleri-True")]
        [HttpPost]
        public ActionResult RequestOperations(RequestForViewModel rfvm)
        {
            var updatingdata = db.REQUESTS.Find(rfvm.request.ID);
            if (updatingdata != null)
            {
                updatingdata.RequestStatusID = rfvm.request.RequestStatusID;

            }
            db.SaveChanges();
            return RedirectToAction("AllRequests");
        }
        [Authorize(Roles = "Arıza/Talep Süreç İşlemleri-True")]
        [HttpGet]
        public ActionResult AddRequestProcess()
        {
            return View();
        }
        [Authorize(Roles = "Arıza/Talep Süreç İşlemleri-True")]
        [HttpPost]
        public ActionResult AddRequestProcess(RequestForViewModel rp)
        {
            rp.requestProcess.RequestID = (int)TempData["RequestID"];
            rp.requestProcess.ProcessTime = DateTime.Now;
            rp.requestProcess.ResponsibleUserID = (int)Session["LoginUserID"];
            db.REQUESTPROCESSES.Add(rp.requestProcess);
            db.SaveChanges();
            return RedirectToAction("RequestDetail/" + rp.requestProcess.RequestID);
        }
        [Authorize(Roles = "Süreç Silme-True")]
        public ActionResult DeleteRequestProcess(int id)
        {
            var deleteRP = db.REQUESTPROCESSES.Find(id);
            db.REQUESTPROCESSES.Remove(deleteRP);
            db.SaveChanges();
            return RedirectToAction("RequestDetail/" + (int)TempData["RequestID"]);
        }
        [Authorize(Roles = "Süreç Silme-True")]
        public ActionResult DeleteMyRequest(REQUEST req)
        {
            var model = db.REQUESTS.OrderByDescending(x => x.AddedTime).ToList();
            var deleteMyRequest = db.REQUESTS.Find(req.ID);
            db.REQUESTS.Remove(deleteMyRequest);
            db.SaveChanges();
            return RedirectToAction("MyRequests", model);
        }
        [Authorize(Roles = "Departman Arıza Kaydı/Talepler-True")]
        public ActionResult MyDepartmentRequests()
        {
            var model = db.REQUESTS.ToList();
            return View("MyDepartmentRequests", model);
        }

    }
}