using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WMarket.Models;

namespace WMarket.ViewModels
{
    public class OrderView
    {
        public Customer Customer { get; set; }
        public List<ProductOrder> Products { get; set; }
        public ProductOrder ProductOrder { get; set; }
    }
}