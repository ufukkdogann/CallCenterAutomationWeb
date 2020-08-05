using CallCenterAutomation.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterAutomation.ViewModels
{
    public class RequestForViewModel
    {
        public List<REQUESTHEADER> requestHeaderList { get; set; }
        public List<REQUESTSTATUS> requestStatusList { get; set; }
        public List<REQUEST> requestList { get; set; }
        public REQUEST request { get; set; }
        public USER user { get; set; }
        public REQUESTSTATUS requestStatus { get; set; }
        public REQUESTHEADER requestHeader { get; set; }
        public List<USER> userList { get; set; }
        public REQUESTPROCESS requestProcess { get; set; }
        public List<REQUESTPROCESS> requestProcessesList { get; set; }
    }
}