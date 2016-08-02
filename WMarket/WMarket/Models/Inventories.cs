using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    public class Inventories
    {
        [Key]
        public int InventoryID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime InventoryDate { get; set; }


        [JsonIgnore]
        public virtual ICollection<InventoryDetail> InventoryDetails { get; set; }
    }
}