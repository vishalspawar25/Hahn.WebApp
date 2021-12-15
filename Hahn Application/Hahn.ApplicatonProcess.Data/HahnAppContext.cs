using Hahn.ApplicatonProcess.Data.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.Data
{
    
    public class HahnAppContext : DbContext
    {
        public HahnAppContext(DbContextOptions<HahnAppContext> options)
            : base(options)
        {
        }
         public DbSet<Applicant> Applicants { get; set; }
    }
}
