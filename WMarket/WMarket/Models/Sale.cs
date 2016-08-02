using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    public class Sale
    {
        [Key]
        public int SaleID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateSale { get; set; }

        [Required]
        public int CustomerID { get; set; }

        //Lado UNO
        [JsonIgnore]
        public virtual Customer Customer { get; set; }

        //Lado Varios   
        [JsonIgnore]
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
    }
}