using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOrder { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }

        //Lado UNO
        [JsonIgnore]
        public virtual Customer Customer { get; set; }
        
        //Lado Varios        
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}