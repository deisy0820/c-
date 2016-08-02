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
    public class ProductsController : Controller
    {
        private WMarketContext db = new WMarketContext();

        // GET: /Products/
        [Authorize(Roles="View")]
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: /Products/Details/5
        [Authorize(Roles = "View")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Products/Create
        [Authorize(Roles = "Create")]
        public ActionResult Create()
        {
            var taxes = db.Taxes.ToList();
            taxes.Add(new Tax { TaxID = 0, Description = "[Seleccione un tipo de impuesto]" });
            taxes = taxes.OrderBy(c => c.Description).ToList();

            ViewBag.TaxID = new SelectList(taxes, "TaxID", "Description");

            var categories = db.Categories.ToList();
            categories.Add(new Category { CategoryID = 0, Description = "[Seleccione un tipo de Categoria]" });
            categories = categories.OrderBy(c => c.Description).ToList();

            ViewBag.CategoryID = new SelectList(categories, "CategoryID", "Description");
            return View();
        }

        // POST: /Products/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Description,Price,LastBuy,Stock,Remarks,TaxID,CategoryID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: /Products/Edit/5
        [Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.TaxID = new SelectList(db.Taxes, "TaxID", "Description", product.TaxID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Description", product.CategoryID);
            return View(product);
        }

        // POST: /Products/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Description,Price,LastBuy,Stock,Remarks,TaxID,CategoryID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: /Products/Delete/5
        [Authorize(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
