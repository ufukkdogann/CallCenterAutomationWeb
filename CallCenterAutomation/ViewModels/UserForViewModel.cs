using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CallCenterAutomation.Models;
using CallCenterAutomation.Models.Entity;

namespace CallCenterAutomation.ViewModels
{
    public class UserForViewModel
    {
        public List<USER> userList { get; set; }
        public USER user { get; set; }
        public List<ROLEMASTER> roleMasterList { get; set; }
        public List<USERROLEMAPPING> userRoleMappingList { get; set; }
        public List<EMPLOYEE> employeeList { get; set; }
        public USERROLEMAPPING userRoleMapping { get; set; }


    }
}