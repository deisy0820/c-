using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMarket.Models;
using WMarket.ViewModels;

namespace WMarket.Controllers
{
    public class ShoppingController : Controller
    {
        //
        // GET: /Shopping/
        private WMarketContext db = new WMarketContext();
        //
        // GET: /Inventories/

        [Authorize(Roles = "Shoppings")]
        public ActionResult NewShopping()
        {
            var orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.Products = new List<ProductOrder>();

            Session["orderView"] = orderView;

            var list = db.Customers.ToList();
            list.Add(new Customer { CustomerID = 0, FirsName = "[Seleccione un Cliente]" });
            list = list.OrderBy(c => c.FullName).ToList();

            ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName");

            return View(orderView);
        }

        [HttpPost]
        public ActionResult NewShopping(OrderView orderView)
        {
            orderView = Session["orderView"] as OrderView;

            if (orderView.Products.Count == 0)
            {
                ViewBag.Error = "Debe ingresar detalle";
                return View(orderView);
            }

            var ShoppingID = 0;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var shopping = new Shopping()
                    {
                        LastBuy = DateTime.Now
                    };

                    db.Shopping.Add(shopping);
                    db.SaveChanges();

                    ShoppingID = db.Shopping.ToList().Select(o => o.ShoppingID).Max();

                    foreach (var item in orderView.Products)
                    {
                        Product product = db.Products.ToList().Find(c => c.ProductID == item.ProductID);

                        //product.LastBuy = item.LastBuy;
                        product.Stock += item.Quantity;
                        db.SaveChanges();

                        var shoppingDetails = new ShoppingDetail
                        {
                            ShoppingID = ShoppingID,
                            ProductID = item.ProductID,
                            Description = item.Description,
                            Quantity = item.Quantity
                        };

                        db.ShoppingDetails.Add(shoppingDetails);
                    }

                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ViewBag.Error = "ERROR " + ex.Message;
                    return View(orderView);
                }
            }

            orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.Products = new List<ProductOrder>();
            Session["orderView"] = orderView;

            ViewBag.Message = "Producto ingresado exitosamente";
            return View(orderView);
        }

        public ActionResult AddShopping()
        {
            var list = db.Products.ToList();
            list.Add(new ProductOrder { ProductID = 0, Description = "[Seleccione un Producto]" });
            list = list.OrderBy(c => c.Description).ToList();

            ViewBag.ProductID = new SelectList(list, "ProductID", "Description");

            return View();
        }

        [HttpPost]
        public ActionResult AddShopping(ProductOrder productOrder)
        {
            var orderView = Session["orderView"] as OrderView;

            var productID = int.Parse(Request["ProductID"]);

            if (productID == 0)
            {
                var list = db.Products.ToList();
                list.Add(new ProductOrder { ProductID = 0, Description = "[Seleccione un Producto]" });
                list = list.OrderBy(c => c.Description).ToList();

                ViewBag.ProductID = new SelectList(list, "ProductID", "Description");
                ViewBag.Error = "Debe Ingresar un producto";

                productOrder = new ProductOrder();
                return View(productOrder);
            }

            var product = db.Products.Find(productID);

            if (product == null)
            {
                var list = db.Products.ToList();
                list.Add(new ProductOrder { ProductID = 0, Description = "[Seleccione un Producto]" });
                list = list.OrderBy(c => c.Description).ToList();

                ViewBag.ProductID = new SelectList(list, "ProductID", "Description");
                ViewBag.Error = "Producto no existe";

                return View(productOrder);

            }

            productOrder = orderView.Products.Find(p => p.ProductID == productID);

            if (productOrder == null)
            {
                productOrder = new ProductOrder
                {
                    Description = product.Description,
                    Price = product.Price,
                    ProductID = product.ProductID,
                    Quantity = float.Parse(Request["Quantity"])
                };

                orderView.Products.Add(productOrder);
            }
            else
            {
                productOrder.Quantity += float.Parse(Request["Quantity"]);
            }

            return View("NewShopping", orderView);
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