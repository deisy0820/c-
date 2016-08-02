using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    public class ShoppingDetail
    {
        [Key]
        public int ShoppingDetailID { get; set; }

        public int ShoppingID { get; set; }

        public int ProductID { get; set; }

        [Required(ErrorMessage = "Usted debe Ingresar {0}")]
        [Display(Name = "Descripción Producto")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Usted debe ingresar el campo {0}")]
        public float Quantity { get; set; }


        [JsonIgnore]
        public virtual Product Product { get; set; }

        [JsonIgnore]
        public virtual Shopping Shopping { get; set; }
    }
}