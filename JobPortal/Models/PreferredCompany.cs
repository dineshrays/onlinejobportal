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
    
    public partial class PreferredCompany
    {
        public int id { get; set; }
        public int Lookout_id { get; set; }
        public int Company_id { get; set; }
    
        public virtual Lookout Lookout { get; set; }
        public virtual Company Company { get; set; }
    }
}
