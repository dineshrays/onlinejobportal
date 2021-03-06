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
    
    public partial class Recruiter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Recruiter()
        {
            this.RecruiterPlans = new HashSet<RecruiterPlan>();
        }
    
        public int Id { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public int IndusTyId { get; set; }
        public Nullable<bool> Consultant { get; set; }
        public string ContactPersonName { get; set; }
        public string OfficeAddress { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> StateId { get; set; }
        public Nullable<int> CityId { get; set; }
        public string AlternativEmaiId { get; set; }
        public string RecruiterImage { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<bool> Isactive { get; set; }
        public Nullable<bool> Company { get; set; }
        public string Designation { get; set; }
        public Nullable<long> PhoneNo { get; set; }
    
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecruiterPlan> RecruiterPlans { get; set; }
        public virtual IndustryType IndustryType { get; set; }
    }
}
