using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SampleProject.Models;

namespace SampleProject.Controllers
{
    public class CustomerServicesController : Controller
    {
        private DBDHMEntities db = new DBDHMEntities();

        // GET: CustomerServices
        public ActionResult Index()
        {
            var customerServices = db.CustomerServices.Include(c => c.Customer).Include(c => c.Service);
            return View(customerServices.ToList());
        }

        // GET: CustomerServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerService customerService = db.CustomerServices.Find(id);
            if (customerService == null)
            {
                return HttpNotFound();
            }
            return View(customerService);
        }

        // GET: CustomerServices/Create
        public ActionResult Create()
        {
            ViewBag.CId = new SelectList(db.Customers, "CId", "CName");
            ViewBag.SId = new SelectList(db.Services, "SId", "SName");
            return View();
        }

        // POST: CustomerServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CSID,CId,SId")] CustomerService customerService)
        {
            if (ModelState.IsValid)
            {
                db.CustomerServices.Add(customerService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CId = new SelectList(db.Customers, "CId", "CName", customerService.CId);
            ViewBag.SId = new SelectList(db.Services, "SId", "SName", customerService.SId);
            return View(customerService);
        }

        // GET: CustomerServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerService customerService = db.CustomerServices.Find(id);
            if (customerService == null)
            {
                return HttpNotFound();
            }
            ViewBag.CId = new SelectList(db.Customers, "CId", "CName", customerService.CId);
            ViewBag.SId = new SelectList(db.Services, "SId", "SName", customerService.SId);
            return View(customerService);
        }

        // POST: CustomerServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CSID,CId,SId")] CustomerService customerService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CId = new SelectList(db.Customers, "CId", "CName", customerService.CId);
            ViewBag.SId = new SelectList(db.Services, "SId", "SName", customerService.SId);
            return View(customerService);
        }

        // GET: CustomerServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerService customerService = db.CustomerServices.Find(id);
            if (customerService == null)
            {
                return HttpNotFound();
            }
            return View(customerService);
        }

        // POST: CustomerServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerService customerService = db.CustomerServices.Find(id);
            db.CustomerServices.Remove(customerService);
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
    }
}
