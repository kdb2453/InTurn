//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InTurn_Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class JobPosting
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JobPosting()
        {
            this.Applications = new HashSet<Application>();
            this.Employees = new HashSet<Employee>();
            this.MajorRequirements = new HashSet<MajorRequirement>();
        }
    
        public int JobPostingID { get; set; }
        public int EmployerID { get; set; }
        public string Position { get; set; }
        public string Desc { get; set; }
        public Nullable<decimal> Wage { get; set; }
        public string Location { get; set; }
        public short JobType { get; set; }
        public short TimeType { get; set; }
        public string Days { get; set; }
        public Nullable<int> Hours { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Application> Applications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual Employer Employer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MajorRequirement> MajorRequirements { get; set; }
    }
}
