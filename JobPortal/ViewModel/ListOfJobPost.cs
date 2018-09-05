using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JobPortal.Models;

namespace JobPortal.ViewModel
{
    public class ListOfJobPost
    {
       
        public List<JobPost> JobPost { get; set; }
        public List<JobPostFieldsViewModel> JobPostfields { get; set; }
        public JobPostSkill JobPostSkill { get; set; }

        public List<JobLocDetViewModel> city  { get; set; }
        public List<PGViewModel> jobPgDets { get; set; }
        public List<UGViewModel> JobUgDet { get; set; }
        public List<JobSkillDetViewModel> jobPostSkill { get; set; }
            
    }
}