//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CallCenterAutomation.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class PAYMENT
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int DepartmentID { get; set; }
        public string FileNumber { get; set; }
        public long PaymentAmount { get; set; }
        public System.DateTime AddingTime { get; set; }
        public System.DateTime DateTime { get; set; }
    
        public virtual DEPARTMENT DEPARTMENT { get; set; }
        public virtual USER USER { get; set; }
    }
}