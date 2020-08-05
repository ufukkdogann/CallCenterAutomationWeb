using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CallCenterAutomation.Models.Entity;

namespace CallCenterAutomation.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        CallCenterAutomationEntities1 db = new CallCenterAutomationEntities1();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(USER _LoginUser)
        {
           
            if (_LoginUser.Username != null && _LoginUser.Password != null)
            {
                var loginUser = db.USERS.FirstOrDefault(x => x.Username.ToLower() == _LoginUser.Username && x.Password.ToLower() == _LoginUser.Password);
                if (loginUser != null && loginUser.isActive == true)
                {
                    Session["LoginEmployeeNameSurname"] = loginUser.EMPLOYEE.EmployeeName + " " + loginUser.EMPLOYEE.EmployeeSurname; //giriş yapan kullanıcının isim soyismini layout'a taşır.
                    Session["LoginUserDepartmentID"] = loginUser.EMPLOYEE.DEPARTMENT.ID;
                    Session["LoginUserDepartmentName"] = loginUser.EMPLOYEE.DEPARTMENT.DepartmentName;
                    Session["LoginEmployeeID"] = loginUser.EMPLOYEE.ID;
                    Session["LoginUserID"] = loginUser.ID;
                    FormsAuthentication.SetAuthCookie(loginUser.Username, false);
                    return RedirectToAction("Index", "Main");
                }
                else if (loginUser != null && loginUser.isActive == false)
                {
                    ViewBag.Message = "Kullanıcı hesabı deaktif. Sistem yöneticinizle irtibata geçiniz!";
                    ModelState.Clear(); //Textboxları temizler.
                    return View();
                }
                else
                {
                    ViewBag.Message = "Kullanıcı adı veya parola yanlış!";
                    ModelState.Clear();
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Kullanıcı adı veya parola boş bırakılamaz!";
                ModelState.Clear(); //Textboxları temizler.
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session["LoginEmployeeNameSurname"] = null;
            Session["LoginUserDepartmentID"] = null;
            Session["LoginUserDepartmentName"] = null;
            Session["LoginEmployeeID"] = null;
            Session["LoginUserID"] = null;
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
            
        }
    }
}