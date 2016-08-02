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
    public class DocumentTypesController : Controller
    {
        private WMarketContext db = new WMarketContext();

        // GET: /DocumentTypes/
        [Authorize(Roles = "View")]
        public ActionResult Index()
        {
            return View(db.DocumentTypes.ToList());
        }

        // GET: /DocumentTypes/Details/5
        [Authorize(Roles = "View")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentType documenttype = db.DocumentTypes.Find(id);
            if (documenttype == null)
            {
                return HttpNotFound();
            }
            return View(documenttype);
        }

        // GET: /DocumentTypes/Create
        [Authorize(Roles = "Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /DocumentTypes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="DocumentTypeID,Description")] DocumentType documenttype)
        {
            if (ModelState.IsValid)
            {
                db.DocumentTypes.Add(documenttype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(documenttype);
        }

        // GET: /DocumentTypes/Edit/5
        [Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentType documenttype = db.DocumentTypes.Find(id);
            if (documenttype == null)
            {
                return HttpNotFound();
            }
            return View(documenttype);
        }

        // POST: /DocumentTypes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DocumentTypeID,Description")] DocumentType documenttype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documenttype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documenttype);
        }

        // GET: /DocumentTypes/Delete/5
        [Authorize(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentType documenttype = db.DocumentTypes.Find(id);
            if (documenttype == null)
            {
                return HttpNotFound();
            }
            return View(documenttype);
        }

        // POST: /DocumentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentType documenttype = db.DocumentTypes.Find(id);
            db.DocumentTypes.Remove(documenttype);
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
