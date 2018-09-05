using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JobPortal.Models;

namespace JobPortal.ViewModel
{
    public class JobLocDetViewModel
    {
        public int Id { get; set; }
        public int JobPostId { get; set; }
        public int CityID { get; set; }
        public Nullable<bool> Isactive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string CityName { get; set; }
    }
}