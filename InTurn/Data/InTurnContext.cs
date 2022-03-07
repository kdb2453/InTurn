using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InTurn.Data
{
    public class InTurnContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public InTurnContext() : base("name=InTurnContext")
        {
        }

        public System.Data.Entity.DbSet<InTurn_Model.Job> Jobs { get; set; }

        public System.Data.Entity.DbSet<InTurn_Model.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<InTurn_Model.Employer> Employers { get; set; }

        public System.Data.Entity.DbSet<InTurn_Model.JobPosting> JobPostings { get; set; }
    }
}
