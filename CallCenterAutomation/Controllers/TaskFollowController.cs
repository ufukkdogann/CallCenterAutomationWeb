using CallCenterAutomation.Models.Entity;
using CallCenterAutomation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CallCenterAutomation.Controllers
{
    public class TaskFollowController : Controller
    {
        CallCenterAutomationEntities1 db = new CallCenterAutomationEntities1();
        [Authorize(Roles = "Görev Listesi-True")]
        public ActionResult AllTasksList()
        {
            var model = new TaskForViewModel()
            {
                taskList = db.TASKS.OrderByDescending(x => x.AddedTime).ToList(),
                taskProcessList = db.TASKPROCESSES.ToList()
            };
             return View("AllTasksList", model);
        }
        [Authorize(Roles = "Görev İşlemleri-True")]
        [HttpGet]
        public ActionResult TaskOperations()
        {
            var model = new TaskForViewModel
            {
                listUser = db.USERS.ToList(),
                taskList = db.TASKS.ToList(),
                taskProcessList = db.TASKPROCESSES.ToList(),
                taskStatusList = db.TASKSTATUSES.ToList()
            };
            return View("TaskOperations", model);
        }
        [Authorize(Roles = "Görev İşlemleri-True")]

        [HttpPost]
        public ActionResult TaskOperations(TaskForViewModel tsk)
        {
            StringBuilder sb = new StringBuilder();
            if (tsk.task.ID == 0)
            {
                foreach (var item in tsk.listUser)
                {
                    if (item.isChecked)
                    {
                        sb.Append(item.Username + ", ");
                    }
                }
                tsk.task.DepartmentID = (int)Session["LoginUserDepartmentID"];
                tsk.task.ResponsibleUsers = sb.ToString().TrimEnd(new Char[] { ',', ' ' });
                tsk.task.TaskStatusID = 1;//Default olarak "Yapılmadı" işler. => Database tarafında 1: Yapılmadı
                tsk.task.AddedTime = DateTime.Now;
                db.TASKS.Add(tsk.task);
                db.SaveChanges();
                return RedirectToAction("AllTasksList");
            }
            else
            {
                var updatingData = db.TASKS.Find(tsk.task.ID);
                if (updatingData != null)
                {
                    updatingData.Detail = tsk.task.Detail;
                    updatingData.TaskStatusID = tsk.task.TaskStatusID;
                }
                db.SaveChanges();
                return RedirectToAction("AllTasksList");
            }

        }
        [Authorize(Roles = "Görev İşlemleri-True")]
        public ActionResult TaskDetail(TASK task)
        {
            var taskmodel = db.TASKS.Find(task.ID);
            if ((int)Session["LoginUserDepartmentID"] != taskmodel.DepartmentID)
            {
                return RedirectToAction("AllTasksList");
            }
            else
            {
                TempData["TaskID"] = task.ID;
                var model = new TaskForViewModel()
                {
                    listUser = db.USERS.ToList(),
                    taskStatusList = db.TASKSTATUSES.ToList(),
                    taskList = db.TASKS.ToList(),
                    task = db.TASKS.Find(task.ID),
                    taskProcessList = db.TASKPROCESSES.Where(x => x.TaskID == task.ID).OrderByDescending(x => x.LastProcessTime).ToList()
                };
                if (model == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View("TaskOperations", model);
                }
            }
            
        }
        [Authorize(Roles = "Görev İşlemleri-True")]

        [HttpGet]
        public ActionResult AddTaskProcess()
        {
            return View();
        }
        [Authorize(Roles = "Görev İşlemleri-True")]
        [HttpPost]
        public ActionResult AddTaskProcess(TASKPROCESS taskprocess)
        {
            taskprocess.TaskID = (int)TempData["TaskID"];
            taskprocess.LastProcessTime = DateTime.Now;
            taskprocess.ResponsibleUserID = (int)Session["LoginUserID"];
            db.TASKPROCESSES.Add(taskprocess);
            db.SaveChanges();

            return RedirectToAction("TaskDetail/" + taskprocess.TaskID);
        }
        [Authorize(Roles = "Görev İşlemleri-True")]
        public ActionResult DeleteTaskProcess(int id)
        {
            var deletedTP = db.TASKPROCESSES.Find(id);
            db.TASKPROCESSES.Remove(deletedTP);
            db.SaveChanges();
            return RedirectToAction("TaskDetail/" + (int)TempData["TaskID"]);
        }
        [Authorize(Roles = "Görev İşlemleri-True")]
        [HttpPost]
        public ActionResult UpdateResponsibleUser(TaskForViewModel tsk)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in tsk.listUser)
            {
                if (item.isChecked)
                {
                    sb.Append(item.Username + ", ");
                }
            }
            TempData["responsibleUsers"] = sb.ToString().TrimEnd(new Char[] { ',', ' ' });
            var model = new TASK()
            {
                ID = (int)TempData["TaskID"],
                ResponsibleUsers = (string)TempData["responsibleUsers"]
            };
            db.TASKS.Attach(model);
            db.Entry(model).Property(x => x.ResponsibleUsers).IsModified = true;
            db.SaveChanges();
            return RedirectToAction("TaskDetail/" + (int)TempData["TaskID"]);
        }

    }
}