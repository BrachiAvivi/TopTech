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
    
    public partial class Status_tbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Status_tbl()
        {
            this.Call_tbl = new HashSet<Call_tbl>();
            this.History_tbl = new HashSet<History_tbl>();
            this.Visit_tbl = new HashSet<Visit_tbl>();
        }
    
        public int StatusID { get; set; }
        private string statusDetail;

        public string StatusDetail
        {
            get { return statusDetail; }
            set { statusDetail = value.TrimEnd(); }
        }

        public Nullable<int> AssociatedWith { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Call_tbl> Call_tbl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<History_tbl> History_tbl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visit_tbl> Visit_tbl { get; set; }
    }
}
