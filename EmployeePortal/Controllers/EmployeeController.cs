using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CsvHelper;
using EmployeePortal.DAL;
using EmployeePortal.Models;

namespace EmployeePortal.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeContext db = new EmployeeContext();

        // GET: Employee
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,First_Name,Last_Name,Mid_Initial,Phone_Home,Phone_Cell,Extension,IRD,Active")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, First_Name, Last_Name, Mid_Initial, Phone_Home, Phone_Cell, Extension, IRD, Active")] Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (DataException dex )
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    throw dex;
                }
                
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Employee/Export
        public FileContentResult Export()
        {
            var data = db.Employees.ToList();
            var tempFilePath = Path.GetTempPath() + "/EmployeeList.csv";
            using (var writer = new StreamWriter(tempFilePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(data);
            }
            return File(System.IO.File.ReadAllBytes(tempFilePath), "text/csv", $"EmployeeList-{DateTime.Now.ToString("yyyyMMdd-HHmmss")}.csv");
        }
    }
}
