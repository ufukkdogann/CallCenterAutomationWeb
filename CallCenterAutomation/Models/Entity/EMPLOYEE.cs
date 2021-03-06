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
    
    public partial class EMPLOYEE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPLOYEE()
        {
            this.USERS = new HashSet<USER>();
        }
    
        public int ID { get; set; }
        public string TCKNo { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ProximityPhoneNumber { get; set; }
        public string E_Mail { get; set; }
        public Nullable<System.DateTime> DateOfStartJob { get; set; }
        public string IBAN { get; set; }
        public Nullable<int> DegreeOfProximityID { get; set; }
        public Nullable<int> EducationStatusID { get; set; }
        public Nullable<int> BankID { get; set; }
        public Nullable<int> BankAccountTypeID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public Nullable<int> EmployeeTitleID { get; set; }
        public Nullable<int> MarriageStatusID { get; set; }
        public Nullable<int> WorkingStatusID { get; set; }
        public Nullable<int> isAnyUser { get; set; }
    
        public virtual BANKACCOUNTTYPE BANKACCOUNTTYPE { get; set; }
        public virtual BANK BANK { get; set; }
        public virtual COMPANy COMPANy { get; set; }
        public virtual DEPARTMENT DEPARTMENT { get; set; }
        public virtual EDUCATIONSTATUS EDUCATIONSTATUS { get; set; }
        public virtual EMPLOYEETITLE EMPLOYEETITLE { get; set; }
        public virtual MARRIAGESTATUS MARRIAGESTATUS { get; set; }
        public virtual PROXIMITYDEGREE PROXIMITYDEGREE { get; set; }
        public virtual WORKINGSTATUS WORKINGSTATUS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USER> USERS { get; set; }
        public string FullName
        {
            get { return EmployeeName + " " + EmployeeSurname; }
        }
    }
}
