using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMarket.Models;
using WMarket.ViewModels;

namespace WMarket.Controllers
{
    public class OrdersController : Controller
    {
        private WMarketContext db = new WMarketContext();
        //
        // GET: /Orders/
        [Authorize(Roles = "Orders")]
        public ActionResult NewOrder()
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
        public ActionResult NewOrder(OrderView orderView)
        {
            orderView = Session["orderView"] as OrderView;

            var customerID = int.Parse(Request["CustomerID"]);

            if (customerID == 0) 
            {
                var listCliente = db.Customers.ToList();
                listCliente.Add(new Customer { CustomerID = 0, FirsName = "[Seleccione un Cliente]" });
                listCliente = listCliente.OrderBy(c => c.FullName).ToList();

                ViewBag.CustomerID = new SelectList(listCliente, "CustomerID", "FullName");
                ViewBag.Error = "Debe seleccionar un cliente";
                
                return View(orderView);
            }

            var customer = db.Customers.Find(customerID);
            if (customer == null)
            {
                var listCliente = db.Customers.ToList();
                listCliente.Add(new Customer { CustomerID = 0, FirsName = "[Seleccione un Cliente]" });
                listCliente = listCliente.OrderBy(c => c.FullName).ToList();

                ViewBag.CustomerID = new SelectList(listCliente, "CustomerID", "FullName");
                ViewBag.Error = "Debe seleccionar un cliente";

                return View(orderView);
            }


            if (orderView.Products.Count == 0) 
            {

                var listCliente = db.Customers.ToList();
                listCliente.Add(new Customer { CustomerID = 0, FirsName = "[Seleccione un Cliente]" });
                listCliente = listCliente.OrderBy(c => c.FullName).ToList();

                ViewBag.CustomerID = new SelectList(listCliente, "CustomerID", "FullName");
                ViewBag.Error = "Debe ingresar detalle";

                return View(orderView);

            }

            var orderID = 0;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var order = new Order
                    {
                        CustomerID = customerID,
                        DateOrder = DateTime.Now,
                        OrderStatus = OrderStatus.created
                    };

                    db.Orders.Add(order);
                    db.SaveChanges();

                    orderID = db.Orders.ToList().Select(o => o.OrderID).Max();

                    foreach (var item in orderView.Products)
                    {
                        Product product = db.Products.ToList().Find(c => c.ProductID == item.ProductID);

                        product.Stock += item.Quantity;
                        db.SaveChanges();

                        var orderDitail = new OrderDetail
                        {
                            Description = item.Description,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            OrderID = orderID,
                            ProductID = item.ProductID
                        };

                        db.OrderDetails.Add(orderDitail);
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
                    return View(orderView);
                }               
            }

            var listFinal = db.Customers.ToList();
            listFinal.Add(new Customer { CustomerID = 0, FirsName = "[Seleccione un Cliente]" });
            listFinal = listFinal.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(listFinal, "CustomerID", "FullName");
            ViewBag.Message = string.Format("La orden: {0}, grabada exitosamente", orderID);


            orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.Products = new List<ProductOrder>();
            Session["orderView"] = orderView;

            return View(orderView);
        }

        public ActionResult AddProduct()
        {
            var list = db.Products.ToList();
            list.Add(new ProductOrder { ProductID = 0, Description = "[Seleccione un Producto]" });
            list = list.OrderBy(c => c.Description).ToList();

            ViewBag.ProductID = new SelectList(list, "ProductID", "Description");

            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductOrder productOrder)
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

            var listCustomer = db.Customers.ToList();
            listCustomer.Add(new Customer { CustomerID = 0, FirsName = "[Seleccione un Cliente]" });
            listCustomer = listCustomer.OrderBy(c => c.FullName).ToList();

            ViewBag.CustomerID = new SelectList(listCustomer, "CustomerID", "FullName");
            
            return View("NewOrder", orderView);
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