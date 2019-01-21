using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeePortal.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EmployeePortal.DAL
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(): base("EmployeeContext")
        { }

        public DbSet<Employee> Employees { get; set; }
    }
}