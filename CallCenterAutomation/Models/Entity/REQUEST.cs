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
    using System.ComponentModel.DataAnnotations;

    public partial class REQUEST
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public REQUEST()
        {
            this.REQUESTPROCESSES = new HashSet<REQUESTPROCESS>();
        }

        public int ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> RequestHeaderID { get; set; }
        public string RequestExplanation { get; set; }
        public Nullable<System.DateTime> AddedTime { get; set; }

        
        public string Anydesk { get; set; }
        public Nullable<short> DeskNumber { get; set; }
        public Nullable<short> InternalNumber { get; set; }
        public Nullable<int> RequestStatusID { get; set; }

        public virtual REQUESTHEADER REQUESTHEADER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REQUESTPROCESS> REQUESTPROCESSES { get; set; }
        public virtual REQUESTSTATUS REQUESTSTATUS { get; set; }
        public virtual USER USER { get; set; }
    }
}
