using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    public class ShoppingAPI
    {
        public int ShoppingID { get; set; }
        public DateTime DateShopping { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<ShoppingDetail> Details { get; set; }
    }
}