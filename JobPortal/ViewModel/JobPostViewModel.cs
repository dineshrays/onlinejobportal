using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JobPortal.Models;

namespace JobPortal.ViewModel
{
    public class JobPostViewModel
    {

        public JobPost jobPost { get; set; }
        public Skill skill { get; set; }
        public City city { get; set; }
        public JobPgDet jobPgDet { get; set; }
        public JobUgDet jobUgDet { get; set; }
        public Company company { get; set; }
        public FunctionalArea functionalArea { get; set; }
        public Role role { get; set; }
    }
}