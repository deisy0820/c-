using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WMarket.Models;

namespace WMarket.Controllers
{
    public class TaxesAPIController : ApiController
    {
        private WMarketContext db = new WMarketContext();

        // GET api/TaxesAPI
        public IQueryable<Tax> GetTaxes()
        {
            return db.Taxes;
        }

        // GET api/TaxesAPI/5
        [ResponseType(typeof(Tax))]
        public IHttpActionResult GetTax(int id)
        {
            Tax tax = db.Taxes.Find(id);          
            return Ok(tax);
        }

        // PUT api/TaxesAPI/5
        public IHttpActionResult PutTax(int id, Tax tax)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tax.TaxID)
            {
                return BadRequest();
            }

            db.Entry(tax).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaxExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/TaxesAPI
        [ResponseType(typeof(Tax))]
        public IHttpActionResult PostTax(Tax tax)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Taxes.Add(tax);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tax.TaxID }, tax);
        }

        // DELETE api/TaxesAPI/5
        [ResponseType(typeof(Tax))]
        public IHttpActionResult DeleteTax(int id)
        {
            Tax tax = db.Taxes.Find(id);
            if (tax == null)
            {
                return Ok(tax);
            }

            db.Taxes.Remove(tax);
            db.SaveChanges();

            return Ok(tax);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaxExists(int id)
        {
            return db.Taxes.Count(e => e.TaxID == id) > 0;
        }
    }
}