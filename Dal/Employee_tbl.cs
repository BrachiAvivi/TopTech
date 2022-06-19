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
    
    public partial class Employee_tbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee_tbl()
        {
            this.Visit_tbl = new HashSet<Visit_tbl>();
            this.BusinessDay_tbl = new HashSet<BusinessDay_tbl>();
        }
    
        public int EmployeeID { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value.TrimEnd(); }
        }
        public Nullable<System.DateTime> CompanyEntryDate { get; set; }
        public Nullable<decimal> LocationX { get; set; }
        public Nullable<decimal> LocationY { get; set; }
        private string gmail;

        public string Gmail
        {
            get { return gmail; }
            set { gmail = value.TrimEnd(); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value.TrimEnd(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visit_tbl> Visit_tbl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessDay_tbl> BusinessDay_tbl { get; set; }
    }
}
