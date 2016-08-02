using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    public class Tax
    {
        [Key]
        public int TaxID { get; set; }

        [StringLength(30, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        [Required(ErrorMessage = "Usted debe Ingresar {0}")]
        [Display(Name = "Descripción Impuesto")]
        public string Description { get; set; }

        //Lado Varios        
        public virtual ICollection<Product> Products { get; set; }

    }
}