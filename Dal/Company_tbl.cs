//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class Company_tbl
    {
        public int CompanyID { get; set; }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value.TrimEnd(); }
        }

        public Nullable<int> EmployeesNumber { get; set; }
        public Nullable<int> CommitmentForSeveralBusinessDays { get; set; }
        public string ManagementPermissionCode { get; set; }
    }
}
