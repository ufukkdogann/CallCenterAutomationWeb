using CallCenterAutomation.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterAutomation.ViewModels
{
    public class CollectionTrackingForViewModel
    {
        public List<USER> userList { get; set; }
        public USER user { get; set; }
        public List<PAYMENT> paymentList { get; set; }
        public PAYMENT payment { get; set; }
        public List<EXPECTATION> expectationList { get; set; }
        public EXPECTATION expectation { get; set; }
        public DEPARTMENT department { get; set; }
        public List<BANK> bankList { get; set; }
        public EMPLOYEE employee { get; set; }
        public List<EMPLOYEE> employeeList { get; set; }

    }
}