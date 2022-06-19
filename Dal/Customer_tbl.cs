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
    
    public partial class Customer_tbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer_tbl()
        {
            this.Call_tbl = new HashSet<Call_tbl>();
        }
    
        public int CustomerID { get; set; }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value.TrimEnd(); }
        }
        public string Note { get; set; }
        public string Phone { get; set; }
        public Nullable<decimal> LocationX { get; set; }
        public Nullable<decimal> LocationY { get; set; }
        public Nullable<int> Floor { get; set; }
        public Nullable<int> ApartmentNumber { get; set; }
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
        public virtual ICollection<Call_tbl> Call_tbl { get; set; }
    }
}
