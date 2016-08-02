using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMarket.Models;
using WMarket.ViewModels;

namespace WMarket.Controllers
{
    public class SalesController : Controller
    {
        private WMarketContext db = new WMarketContext();
        //
        // GET: /Orders/
        [Authorize(Roles = "Sales")]
        public ActionResult NewSale()
        {

            var saleView = new SaleView();
            saleView.Customer = new Customer();
            saleView.Products = new List<ProductSale>();

            Session["saleView"] = saleView;

            var list = db.Customers.ToList();
            list.Add(new Customer { CustomerID = 0, FirsName = "[Seleccione un Cliente]" });
            list = list.OrderBy(c => c.FullName).ToList();

            ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName");

            return View(saleView);
        }

        [HttpPost]
        public ActionResult NewSale(SaleView saleView)
        {
            saleView = Session["saleView"] as SaleView;

            var customerID = int.Parse(Request["CustomerID"]);

            if (customerID == 0)
            {
                var listCliente = db.Customers.ToList();
                listCliente.Add(new Customer { CustomerID = 0, FirsName = "[Seleccione un Cliente]" });
                listCliente = listCliente.OrderBy(c => c.FullName).ToList();

                ViewBag.CustomerID = new SelectList(listCliente, "CustomerID", "FullName");
                ViewBag.Error = "Debe seleccionar un cliente";

                return View(saleView);
            }

            var customer = db.Customers.Find(customerID);
            if (customer == null)
            {
                var listCliente = db.Customers.ToList();
                listCliente.Add(new Customer { CustomerID = 0, FirsName = "[Seleccione un Cliente]" });
                listCliente = listCliente.OrderBy(c => c.FullName).ToList();

                ViewBag.CustomerID = new SelectList(listCliente, "CustomerID", "FullName");
                ViewBag.Error = "Debe seleccionar un cliente";

                return View(saleView);
            }


            if (saleView.Products.Count == 0)
            {

                var listCliente = db.Customers.ToList();
                listCliente.Add(new Customer { CustomerID = 0, FirsName = "[Seleccione un Cliente]" });
                listCliente = listCliente.OrderBy(c => c.FullName).ToList();

                ViewBag.CustomerID = new SelectList(listCliente, "CustomerID", "FullName");
                ViewBag.Error = "Debe ingresar detalle";

                return View(saleView);

            }

            var saleID = 0;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var sale = new Sale
                    {
                        CustomerID = customerID,
                        DateSale = DateTime.Now,

                    };

                    db.Sales.Add(sale);
                    db.SaveChanges();

                    saleID = db.Sales.ToList().Select(s => s.SaleID).Max();

                    foreach (var item in saleView.Products)
                    {
                        Product product = db.Products.ToList().Find(c => c.ProductID == item.ProductID);

                        product.Stock -= item.Quantity;
                        db.SaveChanges();

                        var saleDitail = new SaleDetail
                        {
                            Description = item.Description,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            SaleID = saleID,
                            ProductID = item.ProductID
                        };

                        db.SaleDetails.Add(saleDitail);
                    }

                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    var listFinalError = db.Customers.ToList();
                    listFinalError.Add(new Customer { CustomerID = 0, FirsName = "[Seleccione un Cliente]" });
                    listFinalError = listFinalError.OrderBy(c => c.FullName).ToList();
                    ViewBag.CustomerID = new SelectList(listFinalError, "CustomerID", "FullName");
                    ViewBag.Error = "ERROR " + ex.Message;
                    return View(saleView);
                }
            }

            var listFinal = db.Customers.ToList();
            listFinal.Add(new Customer { CustomerID = 0, FirsName = "[Seleccione un Cliente]" });
            listFinal = listFinal.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(listFinal, "CustomerID", "FullName");
            ViewBag.Message = string.Format("La orden: {0}, grabada exitosamente", saleID);


            saleView = new SaleView();
            saleView.Customer = new Customer();
            saleView.Products = new List<ProductSale>();
            Session["saleView"] = saleView;

            return View(saleView);
        }

        public ActionResult SaleProduct()
        {
            var list = db.Products.ToList();
            list.Add(new ProductOrder { ProductID = 0, Description = "[Seleccione un Producto]" });
            list = list.OrderBy(c => c.Description).ToList();

            ViewBag.ProductID = new SelectList(list, "ProductID", "Description");

            return View();
        }

        [HttpPost]
        public ActionResult SaleProduct(ProductSale productSale)
        {
            var saleView = Session["saleView"] as SaleView;

            var productID = int.Parse(Request["ProductID"]);

            if (productID == 0)
            {
                var list = db.Products.ToList();
                list.Add(new ProductOrder { ProductID = 0, Description = "[Seleccione un Producto]" });
                list = list.OrderBy(c => c.Description).ToList();

                ViewBag.ProductID = new SelectList(list, "ProductID", "Description");
                ViewBag.Error = "Debe Ingresar un producto";

                return View(productSale);
            }

            var product = db.Products.Find(productID);

            if (product == null)
            {
                var list = db.Products.ToList();
                list.Add(new ProductSale { ProductID = 0, Description = "[Seleccione un Producto]" });
                list = list.OrderBy(c => c.Description).ToList();

                ViewBag.ProductID = new SelectList(list, "ProductID", "Description");
                ViewBag.Error = "Producto no existe";

                return View(productSale);

            }

            productSale = saleView.Products.Find(p => p.ProductID == productID);

            if (productSale == null)
            {
                productSale = new ProductSale
                {
                    Description = product.Description,
                    Price = product.Price,
                    ProductID = product.ProductID,
                    Quantity = float.Parse(Request["Quantity"])
                };

                saleView.Products.Add(productSale);
            }
            else
            {
                productSale.Quantity += float.Parse(Request["Quantity"]);
            }

            var listCustomer = db.Customers.ToList();
            listCustomer.Add(new Customer { CustomerID = 0, FirsName = "[Seleccione un Cliente]" });
            listCustomer = listCustomer.OrderBy(c => c.FullName).ToList();

            ViewBag.CustomerID = new SelectList(listCustomer, "CustomerID", "FullName");

            return View("NewSale", saleView);
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