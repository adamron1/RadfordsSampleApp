using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using EmployeePortal.Models;
using Bogus;

namespace EmployeePortal.DAL
{
    public class EmployeeInitialiser : System.Data.Entity.DropCreateDatabaseIfModelChanges<EmployeeContext>
    {
        protected override void Seed(EmployeeContext context)
        {
            var employees = new List<Employee> { };
            for (int i = 0; i < 15; i++)
            {
                var fake = new Bogus.Person();
                var fake_employee = new Employee
                {
                    First_Name = fake.FirstName,
                    Last_Name = fake.LastName,
                    Phone_Cell = fake.Phone,
                    Active = false
                };
                employees.Add(fake_employee);
            }

            employees.ForEach(e => context.Employees.Add(e));
            context.SaveChanges();
        }
    }
}