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
    
    public partial class History_tbl
    {
        public int HistoryID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int CallID { get; set; }
        public int StatusID { get; set; }
    
        public virtual Call_tbl Call_tbl { get; set; }
        public virtual Status_tbl Status_tbl { get; set; }
    }
}
