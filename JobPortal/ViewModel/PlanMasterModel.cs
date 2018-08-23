using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal.ViewModel
{
    public class PlanMasterModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> ValidFrom { get; set; }
        public Nullable<System.DateTime> ValidTo { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<bool> Isactive { get; set; }
        public long? NoClicks { get; internal set; }
    }
}