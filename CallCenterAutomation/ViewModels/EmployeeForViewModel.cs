using CallCenterAutomation.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterAutomation.ViewModels
{
    public class EmployeeForViewModel
    {
       
        public List<MARRIAGESTATUS> marriageStatus { get; set; }
        public List<BANK> bank { get; set; }
        public List<BANKACCOUNTTYPE> bankAccountType { get; set; }
        public List<EDUCATIONSTATUS> educationStatus { get; set; }
        public List<PROXIMITYDEGREE> proximityDegree { get; set; }
        public List<COMPANy> company { get; set; }
        public List<EMPLOYEETITLE> employeeTitle { get; set; }
        public List<DEPARTMENT> department { get; set; }
        public List<WORKINGSTATUS> workingStatus { get; set; }
        public EMPLOYEE employee { get; set; }
        public List<EMPLOYEE> employeeList { get; set; }

    }
}