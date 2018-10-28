using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleProject.Models;
using System.Data.Entity;
using System.IO;
using System.Net;
using SampleProject.Report;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace SampleProject.Controllers
{
    public class CustomerController : Controller
    {
        DBDHMEntities db = new DBDHMEntities();
        // GET: Customer
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customer/Details/5
              [HttpGet]
        public ActionResult Details(int? id)
        {
            Customer c = db.Customers.Find(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            var Results = from b in db.Services
                          select new
                          {
                              b.SId,
                              b.SName,
                              Checked = ((from ab in db.CustomerServices
                                          where (ab.CId == id) & (ab.SId == b.SId)
                                          select ab).Count() > 0)
                          };

            var MyViewModel = new CustomerViewModel();

            MyViewModel.CId = id.Value;
            MyViewModel.CName = c.CName;
            MyViewModel.Address = c.Address;
            MyViewModel.Email = c.Email;
            MyViewModel.Phone = c.Phone;
            MyViewModel.Photo = c.Photo;

            var MyCheckBoxList = new List<CheckBoxViewModel>();

            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxViewModel { SId = item.SId, SName = item.SName, Checked = item.Checked });
            }

            MyViewModel.Services = MyCheckBoxList;

            return View(MyViewModel);

        }

        // GET: Customer/Create
        [HttpGet]
        public ActionResult Create()
        {
            CustomerViewModel MyViewModel = new CustomerViewModel();
            var Results = from b in db.Services
                          select new
                          {
                              b.SId,
                              b.SName,
                              Checked = false
                          };

           



            var MyCheckBoxList = new List<CheckBoxViewModel>();

            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxViewModel { SId = item.SId, SName = item.SName, Checked = item.Checked });
            }

            MyViewModel.Services = MyCheckBoxList;
            

            return View(MyViewModel);
           
        }


        [HttpPost]
        public ActionResult Create(CustomerViewModel cm)
        {
            HttpPostedFileBase fps = cm.ImageFile;
            if (fps != null) { 
            String fileName = Path.GetFileNameWithoutExtension(cm.ImageFile.FileName);
            String extention = Path.GetExtension(cm.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
            cm.Photo = "~/Images/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Images"), fileName);

            cm.ImageFile.SaveAs(fileName);
        }
            foreach (var item in cm.Services)
            {
                if (item.Checked)
                {
                    db.CustomerServices.Add(new CustomerService() { CId = cm.CId, SId = item.SId });
                }
            }

            Customer MyCustomer = new Customer();
            MyCustomer.CId = cm.CId;
            MyCustomer.CName = cm.CName;
            MyCustomer.Address = cm.Address;
            MyCustomer.Email = cm.Email;
            MyCustomer.Phone = cm.Phone;
            MyCustomer.Photo = cm.Photo;
            if (ModelState.IsValid)
            {
                db.Customers.Add(MyCustomer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(cm);
        }

        // GET: Customer/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer c = db.Customers.Find(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            var Results = from b in db.Services
                          select new
                          {
                              b.SId,
                              b.SName,
                              Checked = ((from ab in db.CustomerServices
                                          where (ab.CId == id) & (ab.SId == b.SId)
                                          select ab).Count() > 0)
                          };

            var MyViewModel = new CustomerViewModel();

            MyViewModel.CId = id.Value;
            MyViewModel.CName = c.CName;
            MyViewModel.Address = c.Address;
            MyViewModel.Email = c.Email;
            MyViewModel.Phone = c.Phone;
            MyViewModel.Photo = c.Photo;

            var MyCheckBoxList = new List<CheckBoxViewModel>();

            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxViewModel { SId = item.SId, SName = item.SName, Checked = item.Checked });
            }

            MyViewModel.Services = MyCheckBoxList;

            return View(MyViewModel);

        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit( int? id,CustomerViewModel c)
        {
            HttpPostedFileBase fps = c.ImageFile1;
                if(fps!=null) { 
                string fileName = Path.GetFileNameWithoutExtension(c.ImageFile1.FileName);
                string extension = Path.GetExtension(c.ImageFile1.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                c.Photo = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                c.ImageFile1.SaveAs(fileName);
            }
            // TODO: Add update logic here
            if (ModelState.IsValid)
                {
                var MyCustomer = db.Customers.Find(c.CId);

                MyCustomer.CId = id.Value;
                MyCustomer.CName = c.CName;
                MyCustomer.Address = c.Address;
                MyCustomer.Email = c.Email;
                MyCustomer.Phone = c.Phone;
                MyCustomer.Photo = c.Photo;

                foreach (var item in db.CustomerServices)
                {
                    if (item.CId == c.CId)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }

                foreach (var item in c.Services)
                {
                    if (item.Checked)
                    {
                        db.CustomerServices.Add(new CustomerService() { CId = c.CId, SId = item.SId });
                    }
                }

                db.Entry(MyCustomer).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
          
            return View(c);
        }

        // GET: Customer/Delete/5
        [HttpGet]
        public ActionResult Delete(int id=0)
        {
            Customer c = db.Customers.Find(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            return View(c);
        }

        // POST: Customer/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirm(int id=0)
        {

            // TODO: Add delete logic here
            try { 
               
                    Customer c = db.Customers.Find(id);
                    db.Customers.Remove(c);
                   db.SaveChanges();

                    return RedirectToAction("Index");

            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        DHMDataSet ds = new DHMDataSet();
        public ActionResult ReportCustomer()
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);

            var connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;


            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT * FROM Customer", conx);

            adp.Fill(ds, ds.Customer.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Report\AllCustomerReport.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("CustomerDataSet", ds.Tables[0]));


            ViewBag.ReportViewer = reportViewer;

            return View();
        }
    }
}  
