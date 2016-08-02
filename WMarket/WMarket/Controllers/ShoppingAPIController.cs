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
    public class ShoppingAPIController : ApiController
    {
        private WMarketContext db = new WMarketContext();

        // GET api/ShoppingAPI
        public IHttpActionResult GetShopping()
        {
            var list = db.Shopping.ToList();
            var ShoppingAPI = new List<ShoppingAPI>();

            foreach (var shopping in list)
            {
                var shoppingAPI = new ShoppingAPI
                {
                    DateShopping = shopping.LastBuy,
                    Details = shopping.ShoppingDetails,
                    ShoppingID = shopping.ShoppingID,
                };

                ShoppingAPI.Add(shoppingAPI);
            }

            return Ok(ShoppingAPI);
        }

        // GET api/ShoppingAPI/5
        [ResponseType(typeof(Shopping))]
        public IHttpActionResult GetShopping(int id)
        {
            Shopping shopping = db.Shopping.Find(id);
            if (shopping == null)
            {
                return NotFound();
            }

            return Ok(shopping);
        }

        // PUT api/ShoppingAPI/5
        public IHttpActionResult PutShopping(int id, Shopping shopping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shopping.ShoppingID)
            {
                return BadRequest();
            }

            db.Entry(shopping).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingExists(id))
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

        // POST api/ShoppingAPI
        [ResponseType(typeof(Shopping))]
        public IHttpActionResult PostShopping(Shopping shopping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Shopping.Add(shopping);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shopping.ShoppingID }, shopping);
        }

        // DELETE api/ShoppingAPI/5
        [ResponseType(typeof(Shopping))]
        public IHttpActionResult DeleteShopping(int id)
        {
            Shopping shopping = db.Shopping.Find(id);
            if (shopping == null)
            {
                return NotFound();
            }

            db.Shopping.Remove(shopping);
            db.SaveChanges();

            return Ok(shopping);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShoppingExists(int id)
        {
            return db.Shopping.Count(e => e.ShoppingID == id) > 0;
        }
    }
}