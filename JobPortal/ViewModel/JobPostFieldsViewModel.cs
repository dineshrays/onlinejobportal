using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JobPortal.Models;

namespace JobPortal.ViewModel
{
    public class JobPostFieldsViewModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
        public int RecruiterId { get; set; }
        public string Description { get; set; }
        public Nullable<int> NoOfvacancies { get; set; }
        public string Location { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string Requirement { get; set; }
        public string SpecifyDoc { get; set; }
        public int IndustryId { get; set; }
        public Nullable<int> FunctionalAreaId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ExpFrom { get; set; }
        public Nullable<int> ExpTo { get; set; }
        public Nullable<decimal> CTCFrom { get; set; }
        public Nullable<decimal> CTCTo { get; set; }
        public string JobTittle { get; set; }

        public string FunctionalAreaName { get; set; }
        public string BusinessStreamName { get; set; }
        public string RoleType { get; set; }
        public string CompanyName { get; set; }
        public byte[] AboutCompany { get; set; }
        public string ContactPerson { get; set; }
        public string CompanyImage { get; set; }
        public string CompanyPresentation { get; set; }
        public string ComapnyWebsite { get; set; }
    }
}