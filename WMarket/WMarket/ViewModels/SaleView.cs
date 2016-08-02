using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WMarket.Models;

namespace WMarket.ViewModels
{
    public class SaleView
    {
        public Customer Customer { get; set; }
        public List<ProductSale> Products { get; set; }
        public ProductSale ProductSale { get; set; }
    }
}