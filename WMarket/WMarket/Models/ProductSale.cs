using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    public class ProductSale : Product
    {
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Usted debe ingresar el campo {0}")]
        [NotMapped]
        public float Quantity { get; set; }

        public decimal Value { get { return Price * (Decimal)Quantity; } }
    }
}