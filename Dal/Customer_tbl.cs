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
        private string name;
        private string note;
        private string phone;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer_tbl()
        {
            this.Call_tbl = new HashSet<Call_tbl>();
        }

        public int CustomerID { get; set; }
        public string Name { get => name; set => name = value.TrimEnd(); }
        public string Note { get => note; set => note = value.TrimEnd(); }
        public string Phone { get => phone; set => phone = value.TrimEnd(); }
        public Nullable<decimal> LocationX { get; set; }
        public Nullable<decimal> LocationY { get; set; }
        public Nullable<int> Floor { get; set; }
        public Nullable<int> ApartmentNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Call_tbl> Call_tbl { get; set; }
    }
}
