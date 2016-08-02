using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    public class Shopping
    {
        [Key]
        public int ShoppingID { get; set; }
        
        [Display(Name = "Compra")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Usted debe Ingresar {0}")]
        public DateTime LastBuy { get; set; }

           
        [JsonIgnore]
        public virtual ICollection<ShoppingDetail> ShoppingDetails { get; set; }
    }
}