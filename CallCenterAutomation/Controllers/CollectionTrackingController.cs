using CallCenterAutomation.Models.Entity;
using CallCenterAutomation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CallCenterAutomation.Controllers
{
    [Authorize]
    public class CollectionTrackingController : Controller
    {
        CallCenterAutomationEntities1 db = new CallCenterAutomationEntities1();
        // GET: CollectionTracking
        [Authorize(Roles = "Tahsilat İşlemleri - Özet Bilgi-True")]

        public ActionResult SummaryPage()
        {
            var model = new CollectionTrackingForViewModel()
            {
                bankList = db.BANKS.ToList(),
                paymentList = db.PAYMENTS.ToList(),
                expectationList = db.EXPECTATIONS.ToList(),
                userList = db.USERS.ToList(), 
                employeeList = db.EMPLOYEES.ToList()
            };
            return View(model);
        }
        #region CollectionTrackingOperationsAndLists
        #region PaymentsArea
        [Authorize(Roles = "Bugüne Ait Ödeme Listesi-True")]

        public ActionResult TodayAllPaymentsList(string searching)
        {
            var model = db.PAYMENTS.Where(x => x.FileNumber.Contains(searching) || searching == null || x.USER.EMPLOYEE.EmployeeName.Contains(searching) || x.USER.EMPLOYEE.EmployeeSurname.Contains(searching)).OrderByDescending(x => x.AddingTime).ToList();
            ViewBag.PageStatus = "Bugüne Ait Ödemeler";
            return View("TodayAllPaymentsList", model);
        }
        [Authorize(Roles = "Tüm Ödemeler Listesi-True")]
        public ActionResult AllPaymentsList(string searching)
        {
            var model = db.PAYMENTS.Where(x => x.FileNumber.Contains(searching) || searching == null || x.USER.EMPLOYEE.EmployeeName.Contains(searching) || x.USER.EMPLOYEE.EmployeeSurname.Contains(searching)).OrderByDescending(x => x.AddingTime).ToList();
            ViewBag.PageStatus = "Tüm Ödemeler";

            return View("AllPaymentsList", model);
        }
        [Authorize(Roles = "Ödeme İşlemleri-True")]

        [HttpGet]
        public ActionResult PaymentOperations()
        {
            var model = new CollectionTrackingForViewModel()
            {
                bankList = db.BANKS.ToList(),
                paymentList = db.PAYMENTS.ToList(),
                expectationList = db.EXPECTATIONS.ToList(),
                userList = db.USERS.ToList(),
                employeeList = db.EMPLOYEES.ToList()
            };

            return View("PaymentOperations", model);
        }
        [Authorize(Roles = "Ödeme İşlemleri-True")]

        [HttpPost]
        public ActionResult PaymentOperations(CollectionTrackingForViewModel CL)
        {
            var depID = (int)Session["LoginUserDepartmentID"];
            var UserID = (int)Session["LoginUserID"];
            var employeeID = (int)Session["LoginEmployeeID"];

            if (CL.payment.ID == 0)
            {
                
                CL.payment.UserID = UserID;
                CL.payment.DepartmentID = depID;
                CL.payment.AddingTime = DateTime.Now;
                CL.payment.DateTime = DateTime.Today.Date;
                db.PAYMENTS.Add(CL.payment);
                db.SaveChanges();
                return RedirectToAction("TodayAllPaymentsList");
            }
            else
            {
                var updatingPayment = db.PAYMENTS.Find(CL.payment.ID);
                if (updatingPayment == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    updatingPayment.FileNumber = CL.payment.FileNumber;
                    updatingPayment.PaymentAmount = CL.payment.PaymentAmount;
                }
                db.SaveChanges();
                return RedirectToAction("TodayAllPaymentsList", "CollectionTracking");
            }
        }
        #endregion

        #region ExpectationsArea
        [Authorize(Roles = "Bugüne Ait Beklenti Listesi-True")]

        public ActionResult TodayAllExpectationsList(string searching)
        {
            var model = db.EXPECTATIONS.Where(x => x.FileNumber.Contains(searching) || searching == null || x.USER.EMPLOYEE.EmployeeName.Contains(searching) || x.USER.EMPLOYEE.EmployeeSurname.Contains(searching)).OrderByDescending(x => x.AddingTime).ToList();
            ViewBag.PageStatus = "Bugüne Ait Beklentiler";
            return View("TodayAllExpectationsList", model);
        }
        [Authorize(Roles = "Tüm Beklentiler Listesi-True")]

        public ActionResult AllExpectationsList(string searching)
        {
            var model = db.EXPECTATIONS.Where(x => x.FileNumber.Contains(searching) || searching == null || x.USER.EMPLOYEE.EmployeeName.Contains(searching) || x.USER.EMPLOYEE.EmployeeSurname.Contains(searching)).OrderByDescending(x => x.AddingTime).ToList();
            ViewBag.PageStatus = "Tüm Beklentiler";

            return View("AllExpectationsList", model);
        }
        [Authorize(Roles = "Beklenti İşlemleri-True")]

        [HttpGet]
        public ActionResult ExpectationOperations()
        {
            var model = new CollectionTrackingForViewModel()
            {
                bankList = db.BANKS.ToList(),
                paymentList = db.PAYMENTS.ToList(),
                expectationList = db.EXPECTATIONS.ToList(),
                userList = db.USERS.ToList(),
                employeeList = db.EMPLOYEES.ToList()
            };

            return View("ExpectationOperations", model);
        }
        [Authorize(Roles = "Beklenti İşlemleri-True")]

        [HttpPost]
        public ActionResult ExpectationOperations(CollectionTrackingForViewModel CL)
        {
            var depID = (int)Session["LoginUserDepartmentID"];
            var UserID = (int)Session["LoginUserID"];
            var employeeID = (int)Session["LoginEmployeeID"];

            if (CL.expectation.ID == 0)
            {
                CL.expectation.UserID = UserID;
                CL.expectation.DepartmentID = depID;
                CL.expectation.AddingTime = DateTime.Now;
                CL.expectation.DateTime = DateTime.Today.Date;
                db.EXPECTATIONS.Add(CL.expectation);
                db.SaveChanges();
                return RedirectToAction("TodayAllExpectationsList");
            }
            else
            {
                var updatingExpectation = db.EXPECTATIONS.Find(CL.expectation.ID);
                if (updatingExpectation == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    updatingExpectation.FileNumber = CL.payment.FileNumber;
                    updatingExpectation.ExpectationAmount = CL.payment.PaymentAmount;
                }
                db.SaveChanges();
                return RedirectToAction("TodayAllExpectationsList", "CollectionTracking");
            }
        }
        #endregion
        #endregion

    }
}