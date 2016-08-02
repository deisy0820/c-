using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMarket.Models;
using WMarket.ViewModels;

namespace WMarket.Controllers
{
    public class InventoriesController : Controller
    {
        private WMarketContext db = new WMarketContext();
        //
        // GET: /Inventories/
        [Authorize(Roles = "Inventories")]
        public ActionResult NewInventory()
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
        public ActionResult NewInventory(OrderView orderView)
        {
            orderView = Session["orderView"] as OrderView;

            if (orderView.Products.Count == 0)
            {
                ViewBag.Error = "Debe ingresar detalle";
                return View(orderView);
            }

            var inventoryID = 0;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var inventory = new Inventories
                    {
                        InventoryDate = DateTime.Now,
                    };

                    db.Inventories.Add(inventory);
                    db.SaveChanges();

                    inventoryID = db.Inventories.ToList().Select(o => o.InventoryID).Max();

                    foreach (var item in orderView.Products)
                    {
                        Product product = db.Products.ToList().Find(c => c.ProductID == item.ProductID);

                        product.Stock += item.Quantity;
                        db.SaveChanges();

                        var inventoryDitail = new InventoryDetail
                        {
                            Description = item.Description,
                            Quantity = item.Quantity,
                            InventoryID = inventoryID,
                            ProductID = item.ProductID
                        };

                        db.InventoryDetails.Add(inventoryDitail);
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

        public ActionResult AddInventory()
        {
            var list = db.Products.ToList();
            list.Add(new ProductOrder { ProductID = 0, Description = "[Seleccione un Producto]" });
            list = list.OrderBy(c => c.Description).ToList();

            ViewBag.ProductID = new SelectList(list, "ProductID", "Description");

            return View();
        }

        [HttpPost]
        public ActionResult AddInventory(ProductOrder productOrder)
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

            return View("NewInventory", orderView);
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