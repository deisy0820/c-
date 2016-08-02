using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WMarket.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [StringLength(30, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        [Required(ErrorMessage = "Usted debe Ingresar {0}")]
        [Display(Name = "Descripción Producto")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Usted debe ingresar el campo {0}")]
        public decimal Price { get; set; }

        [Display(Name = "Ultima compra")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Usted debe Ingresar {0}")]
        public DateTime LastBuy { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public float Stock { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [Display(Name = "Tipo Impuesto")]
        public int TaxID { get; set; }

        [Display(Name = "Tipo Impuesto")]
        public int CategoryID { get; set; }

        //Lado Varios
        [JsonIgnore]
        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [JsonIgnore]
        public virtual ICollection<InventoryDetail> InventoryDetails { get; set; }

        [JsonIgnore]
        public virtual ICollection<ShoppingDetail> ShoppingDetails { get; set; }

        //Lado Uno
        [JsonIgnore]
        public virtual Category Category { get; set; }

        [JsonIgnore]
        public virtual Tax Tax { get; set; }

    }
}