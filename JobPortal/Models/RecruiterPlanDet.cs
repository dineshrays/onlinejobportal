//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JobPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RecruiterPlanDet
    {
        public int Id { get; set; }
        public int RecruiterPlanId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<long> ContactNo { get; set; }
        public string EmailId { get; set; }
        public Nullable<bool> Isactive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public int SubUserCount { get; set; }


        public virtual RecruiterPlan RecruiterPlan { get; set; }
    }
}
