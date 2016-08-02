using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }
        public string Name { get; set; }
        public string ContactFirsName { get; set; }
        public string ContactLastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }


        //Lado Varios
        [JsonIgnore]
        public virtual ICollection<Shopping> Shoppings { get; set; }

        [JsonIgnore]
        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }
    }
}