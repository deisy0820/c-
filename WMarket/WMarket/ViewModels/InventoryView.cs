using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WMarket.Models;

namespace WMarket.ViewModels
{
    public class InventoryView
    {
        public Customer Customer { get; set; }
        public List<ProductInventory> Products { get; set; }
        public ProductInventory ProductInventory { get; set; }
    }
}