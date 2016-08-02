using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    public class SaleAPI
    {
        public int SaleID { get; set; }
        public DateTime DateSale { get; set; }
        public Customer Customer { get; set; }
        public ICollection<SaleDetail> Details { get; set; }
    }
}