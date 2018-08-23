using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal.ViewModel
{
    public class UGViewModel
    {
        public int value { get; set; }
        public string text { get; set; }
        public Nullable<bool> Isactive { get; set; }
        public Nullable<System.DateTime> CreatedON { get; set; }
        public string ModifiedON { get; set; }
    }
}