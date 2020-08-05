using CallCenterAutomation.Models.Entity;
using CallCenterAutomation.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CallCenterAutomation.Controllers
{
    public class UserController : Controller
    {

        CallCenterAutomationEntities1 db = new CallCenterAutomationEntities1();
        #region Actions
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Kullanıcı Listesi-True")]
        [HttpGet]
        public ActionResult UserAllList(string searching)
        {
            var model = db.USERS.Where(x => x.Username.Contains(searching) || x.EMPLOYEE.EmployeeName.Contains(searching) || x.EMPLOYEE.EmployeeSurname.Contains(searching) || searching == null).OrderBy(x => x.Username).ToList();
            ViewBag.UserListStatus = "Tüm Kullanıcılar";
            return View("UserAllList", model);
        }
        [Authorize(Roles = "Kullanıcı İşlemleri-True")]
        [HttpGet]
        public ActionResult UserOperations()
        {
            ViewBag.UserOperationsName = "Kullanıcı Ekle";
            var model = new UserForViewModel()
            {
                userList = db.USERS.ToList(),
                roleMasterList = db.ROLEMASTERS.ToList(),
                userRoleMappingList = db.USERROLEMAPPINGS.ToList(),
                employeeList = db.EMPLOYEES.Where(x => x.isAnyUser == 0).ToList(),
            };
            return View("UserOperations", model);
        }
        public JsonResult CheckUsernameAvailability(string username)
        {
            System.Threading.Thread.Sleep(200);
            var searchData = db.USERS.Where(c => c.Username == username).SingleOrDefault();
            if (searchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
        [Authorize(Roles = "Kullanıcı İşlemleri-True")]
        [HttpPost]
        public ActionResult UserOperations(UserForViewModel _user)
        {
            var existsUsername = db.USERS.Where(c => c.Username == _user.user.Username).SingleOrDefault();
            if (_user.user.ID == 0)
            {
                if (existsUsername != null)
                {
                    ViewBag.UserOperationsName = "Kullanıcı Ekle";
                    var model = new UserForViewModel()
                    {
                        userList = db.USERS.ToList(),
                        roleMasterList = db.ROLEMASTERS.ToList(),
                        userRoleMappingList = db.USERROLEMAPPINGS.ToList(),
                        employeeList = db.EMPLOYEES.Where(x => x.isAnyUser == 0).ToList(),
                    };
                    return View("UserOperations", model);

                }
                else
                {
                    db.USERS.Add(_user.user);
                    var employee = db.EMPLOYEES.FirstOrDefault(x => x.ID == _user.user.EmployeeID);
                    employee.isAnyUser = 1;
                    //Yetki alanı ile ilgili | Varsayılan olarak tüm yetkileri ekler ve false olarak oluşturur.
                    var roleList = db.ROLEMASTERS.ToList();
                    for (int i = 0; i < roleList.Count; i++)
                    {
                        var t = new USERROLEMAPPING()
                        {
                            RoleID = roleList[i].ID,
                            UserID = _user.user.ID,
                            isActive = false
                        };
                        db.USERROLEMAPPINGS.Add(t);
                    }
                    //
                    db.SaveChanges();
                    return RedirectToAction("UserAllList");
                }
            }
            else
            {
                var updatingUser = db.USERS.Find(_user.user.ID);
                var userRoleMapping = db.USERROLEMAPPINGS.Where(x => x.UserID == _user.user.ID).ToList();
                if (updatingUser == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    if (existsUsername != null)
                    {
                        var roleList = db.ROLEMASTERS.ToList();
                        for (int i = 0; i < userRoleMapping.Count; i++)
                        {
                            var t = userRoleMapping[i];
                            db.USERROLEMAPPINGS.Remove(t);
                        }
                        
                        for (int i = 0; i < roleList.Count; i++)
                        {
                            var x = new USERROLEMAPPING()
                            {
                                RoleID = roleList[i].ID,
                                UserID = _user.user.ID,
                                isActive = _user.userRoleMappingList[i].isActive
                            };
                            db.USERROLEMAPPINGS.Add(x);
                        }
                        updatingUser.Password = _user.user.Password;
                        updatingUser.EmployeeID = _user.user.EmployeeID;
                        updatingUser.isActive = _user.user.isActive;
                    }
                    else
                    {
                        var roleList = db.ROLEMASTERS.ToList();
                        for (int i = 0; i < userRoleMapping.Count; i++)
                        {
                            var t = userRoleMapping[i];
                            db.USERROLEMAPPINGS.Remove(t);
                        }
                        for (int i = 0; i < roleList.Count; i++)
                        {
                            var x = new USERROLEMAPPING()
                            {
                                RoleID = roleList[i].ID,
                                UserID = _user.user.ID,
                                isActive = _user.userRoleMappingList[i].isActive
                            };
                            db.USERROLEMAPPINGS.Add(x);
                        }

                        updatingUser.Username = _user.user.Username;
                        updatingUser.Password = _user.user.Password;
                        updatingUser.EmployeeID = _user.user.EmployeeID;
                        updatingUser.isActive = _user.user.isActive;
                    }

                }
                db.SaveChanges();
                return RedirectToAction("UserAllList", "User");
            }
        }
        [Authorize(Roles = "Kullanıcı İşlemleri-True")]

        public ActionResult UserUpdate(USER _user)//Projenin yediği var mı bu son halinin ? yok ama alalım
        {
            ViewBag.UserOperationsName = "Kullanıcı Güncelle";
            var model = new UserForViewModel()
            {
                userList = db.USERS.ToList(),
                roleMasterList = db.ROLEMASTERS.ToList(),
                userRoleMappingList = db.USERROLEMAPPINGS.Where(x => x.UserID == _user.ID).ToList(),
                employeeList = db.EMPLOYEES.ToList(),
                user = db.USERS.Find(_user.ID)
            };
            if (model == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View("UserOperations", model);
            }
        }
        #endregion

    }
}