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
    public class HumanResourcesController : Controller
    {
        CallCenterAutomationEntities1 db = new CallCenterAutomationEntities1();
        #region Actions
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult CheckEmployeeTCAvailability(string TC)
        {
            System.Threading.Thread.Sleep(200);
            var searchData = db.EMPLOYEES.Where(c => c.TCKNo == TC).SingleOrDefault();
            if (searchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
        [Authorize(Roles = "Personel Listesi-True")]
        [HttpGet]
        public ActionResult EmployeeAllList(string searching)
        {
            var model = db.EMPLOYEES.Where(x => x.EmployeeName.Contains(searching) || searching == null || x.EmployeeSurname.Contains(searching) || x.EMPLOYEETITLE.EmployeeTitleName.Contains(searching) || x.TCKNo.Contains(searching)).OrderBy(x => x.EmployeeName).ToList();

            ViewBag.EmployeListStatus = "Tüm Çalışanlar";
            return View("EmployeeAllList", model);
        }

        [Authorize(Roles = "Personel İşlemleri-True")]

        [HttpGet]
        public ActionResult EmployeeOperations()
        {
            ViewBag.EmployeeOperations = "Personel Ekle";
            var model = new EmployeeForViewModel()
            {
                department = db.DEPARTMENTS.ToList(),
                bank = db.BANKS.ToList(),
                bankAccountType = db.BANKACCOUNTTYPES.ToList(),
                company = db.COMPANIES.ToList(),
                educationStatus = db.EDUCATIONSTATUSES.ToList(),
                employeeTitle = db.EMPLOYEETITLES.ToList(),
                marriageStatus = db.MARRIAGESTATUSES.ToList(),
                proximityDegree = db.PROXIMITYDEGREES.ToList(),
                workingStatus = db.WORKINGSTATUSES.ToList(),
                employeeList = db.EMPLOYEES.ToList()
            };
            return View("EmployeeOperations", model);
        }
        [Authorize(Roles = "Personel İşlemleri-True")]

        [HttpPost]
        public ActionResult EmployeeOperations(EmployeeForViewModel Emp)
        {
            var existsEmployee = db.EMPLOYEES.Where(x => x.TCKNo == Emp.employee.TCKNo).SingleOrDefault();
            if (Emp.employee.ID == 0)
            {
                if (existsEmployee != null)
                {
                    var model = new EmployeeForViewModel()
                    {
                        department = db.DEPARTMENTS.ToList(),
                        bank = db.BANKS.ToList(),
                        bankAccountType = db.BANKACCOUNTTYPES.ToList(),
                        company = db.COMPANIES.ToList(),
                        educationStatus = db.EDUCATIONSTATUSES.ToList(),
                        employeeTitle = db.EMPLOYEETITLES.ToList(),
                        marriageStatus = db.MARRIAGESTATUSES.ToList(),
                        proximityDegree = db.PROXIMITYDEGREES.ToList(),
                        workingStatus = db.WORKINGSTATUSES.ToList(),
                        employeeList = db.EMPLOYEES.ToList()
                    };

                    return View("EmployeeOperations", model);
                }
                else
                {
                    Emp.employee.isAnyUser = 0; //0: Kullanıcısı yok 1: Kullanıcısı var || Yeni oluşturulan personel kaydının knullanıcısı olmadığı için varsayılan olarak 0 atanır.
                    db.EMPLOYEES.Add(Emp.employee);
                    db.SaveChanges();
                    return RedirectToAction("EmployeeAllList");
                }
            }
            else
            {
                var updatingEmployee = db.EMPLOYEES.Find(Emp.employee.ID);
                if (updatingEmployee == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    if (existsEmployee != null)
                    {
                        updatingEmployee.EmployeeName = Emp.employee.EmployeeName;
                        updatingEmployee.EmployeeSurname = Emp.employee.EmployeeSurname;
                        updatingEmployee.EmployeeTitleID = Emp.employee.EmployeeTitleID;
                        updatingEmployee.E_Mail = Emp.employee.E_Mail;
                        updatingEmployee.IBAN = Emp.employee.IBAN;
                        updatingEmployee.MarriageStatusID = Emp.employee.MarriageStatusID;
                        updatingEmployee.PhoneNumber = Emp.employee.PhoneNumber;
                        updatingEmployee.ProximityPhoneNumber = Emp.employee.ProximityPhoneNumber;
                        updatingEmployee.SocialSecurityNumber = Emp.employee.SocialSecurityNumber;
                        updatingEmployee.WorkingStatusID = Emp.employee.WorkingStatusID;
                        updatingEmployee.Address = Emp.employee.Address;
                        updatingEmployee.BankID = Emp.employee.BankID;
                        updatingEmployee.BankAccountTypeID = Emp.employee.BankAccountTypeID;
                        updatingEmployee.BirthDate = Emp.employee.BirthDate;
                        updatingEmployee.CompanyID = Emp.employee.CompanyID;
                        updatingEmployee.DateOfStartJob = Emp.employee.DateOfStartJob;
                        updatingEmployee.DegreeOfProximityID = Emp.employee.DegreeOfProximityID;
                        updatingEmployee.DepartmentID = Emp.employee.DepartmentID;
                        updatingEmployee.EducationStatusID = Emp.employee.EducationStatusID;
                    }
                    else
                    {
                        updatingEmployee.EmployeeName = Emp.employee.EmployeeName;
                        updatingEmployee.EmployeeSurname = Emp.employee.EmployeeSurname;
                        updatingEmployee.EmployeeTitleID = Emp.employee.EmployeeTitleID;
                        updatingEmployee.E_Mail = Emp.employee.E_Mail;
                        updatingEmployee.IBAN = Emp.employee.IBAN;
                        updatingEmployee.MarriageStatusID = Emp.employee.MarriageStatusID;
                        updatingEmployee.PhoneNumber = Emp.employee.PhoneNumber;
                        updatingEmployee.ProximityPhoneNumber = Emp.employee.ProximityPhoneNumber;
                        updatingEmployee.SocialSecurityNumber = Emp.employee.SocialSecurityNumber;
                        updatingEmployee.TCKNo = Emp.employee.TCKNo;
                        updatingEmployee.WorkingStatusID = Emp.employee.WorkingStatusID;
                        updatingEmployee.Address = Emp.employee.Address;
                        updatingEmployee.BankID = Emp.employee.BankID;
                        updatingEmployee.BankAccountTypeID = Emp.employee.BankAccountTypeID;
                        updatingEmployee.BirthDate = Emp.employee.BirthDate;
                        updatingEmployee.CompanyID = Emp.employee.CompanyID;
                        updatingEmployee.DateOfStartJob = Emp.employee.DateOfStartJob;
                        updatingEmployee.DegreeOfProximityID = Emp.employee.DegreeOfProximityID;
                        updatingEmployee.DepartmentID = Emp.employee.DepartmentID;
                        updatingEmployee.EducationStatusID = Emp.employee.EducationStatusID;
                    }
                }

                db.SaveChanges();
                return RedirectToAction("EmployeeAllList", "HumanResources");
            }

        }
        [Authorize(Roles = "Personel İşlemleri-True")]

        public ActionResult EmployeeUpdate(int id)
        {
            ViewBag.EmployeeOperations = "Personel Güncelle";
            var model = new EmployeeForViewModel()
            {
                department = db.DEPARTMENTS.ToList(),
                bank = db.BANKS.ToList(),
                bankAccountType = db.BANKACCOUNTTYPES.ToList(),
                company = db.COMPANIES.ToList(),
                educationStatus = db.EDUCATIONSTATUSES.ToList(),
                employeeTitle = db.EMPLOYEETITLES.ToList(),
                marriageStatus = db.MARRIAGESTATUSES.ToList(),
                proximityDegree = db.PROXIMITYDEGREES.ToList(),
                workingStatus = db.WORKINGSTATUSES.ToList(),
                employeeList = db.EMPLOYEES.ToList(),
                employee = db.EMPLOYEES.Find(id)
            };
            if (model == null)
            {
                return HttpNotFound();
            }
            return View("EmployeeOperations", model);
        }
        #endregion

    }
}