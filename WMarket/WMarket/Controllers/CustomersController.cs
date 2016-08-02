using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WMarket.Models;

namespace WMarket.Controllers
{
    public class CustomersController : Controller
    {
        private WMarketContext db = new WMarketContext();

        // GET: /Customers/        
        [Authorize(Roles="View")]
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.DocumentType);
            return View(customers.ToList());
        }

        // GET: /Customers/Details/5
        [Authorize(Roles = "View")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: /Customers/Create
        [Authorize(Roles = "Create")]
        public ActionResult Create()
        {
            var list = db.DocumentTypes.ToList();
            list.Add(new DocumentType { DocumentTypeID = 0, Description = "[Seleccione un tipo de documento]" });
            list = list.OrderBy(c => c.Description).ToList();

            ViewBag.DocumentTypeID = new SelectList(list, "DocumentTypeID", "Description");
            return View();
        }

        // POST: /Customers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CustomerID,FirsName,LastName,Phone,Address,Email,Document,DocumentTypeID")] Customer customer)
        {
            if (ModelState.IsValid && customer.DocumentTypeID != 0)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");            
            }

            ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "Description", customer.DocumentTypeID);
            return View(customer);
        }

        // GET: /Customers/Edit/5
        [Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "Description", customer.DocumentTypeID);
            return View(customer);
        }

        // POST: /Customers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CustomerID,FirsName,LastName,Phone,Address,Email,Document,DocumentTypeID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "Description", customer.DocumentTypeID);
            return View(customer);
        }

        // GET: /Customers/Delete/5
        [Authorize(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: /Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                Customer customer = db.Customers.Find(id);

                try
                {
                    db.Customers.Remove(customer);
                    db.SaveChanges();
                    transaction.Commit();
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    transaction.Rollback();                    
                    ViewBag.Error = "ERROR " + ex.Message;
                    return View(customer);
                }
            }
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
