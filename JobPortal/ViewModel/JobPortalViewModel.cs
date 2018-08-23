using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JobPortal.Models;

namespace JobPortal.ViewModel
{
    public class JobPortalViewModel
    {
         public User_type usertype { get; set; }
        public UserDetail userdetails { get; set; }
        public UserEducation education { get; set; }
        public UserExperience experience { get; set; }
        public Skill skills { get; set; }
        public Company company { get; set; }
        public PreferredCompany prefcompny { get; set; }
        public Location location { get; set; }
        public PreferredLocation preflocatn { get; set; }
        public Lookout lookout { get; set; }
        public Shift shift { get; set; }
        public WorkShift workshifts { get; set; }
        public PersonalInfo personalInformation { get; set; }
        public Certification certificates { get; set; }
        public PremiumColldetail premiumColldetail { get; set; }
        public primecollege primecollege { get; set; }
        public Country country { get; set; }
        public State state { get; set; }
        public City city { get; set; }
      
        public List<UserEducation> userEducations { get; set; }
        public List<UserDetail> UserDetails { get; set; }
    }
}