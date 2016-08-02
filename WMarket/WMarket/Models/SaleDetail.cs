﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    public class SaleDetail
    {
        [Key]
        public int SaleDetailID { get; set; }

        public int SaleID { get; set; }

        public int ProductID { get; set; }

        [Required(ErrorMessage = "Usted debe Ingresar {0}")]
        [Display(Name = "Descripción Producto")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Usted debe ingresar el campo {0}")]
        public decimal Price { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Usted debe ingresar el campo {0}")]
        public float Quantity { get; set; }

        //Lado UNO
        [JsonIgnore]
        public virtual Sale Sale { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}