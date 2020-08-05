using CallCenterAutomation.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterAutomation.ViewModels
{
    public class TaskForViewModel
    {
        public List<TASK> taskList { get; set; }
        public TASK task { get; set; }
        public List<TASKSTATUS> taskStatusList { get; set; }
        public TASKSTATUS taskStatus { get; set; }

        public List<TASKPROCESS> taskProcessList { get; set; }
        public TASKPROCESS taskProcess { get; set; }
        public USER user { get; set; }
        public List<USER> listUser { get; set; }
        public List<EMPLOYEE> listEmployee { get; set; }
        public EMPLOYEE employee {get;set;}

    }
}