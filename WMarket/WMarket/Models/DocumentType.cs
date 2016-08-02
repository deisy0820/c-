using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WMarket.Models
{
    public class DocumentType
    {
        [Key]
        public int DocumentTypeID { get; set; }

        [Display(Name = "Documento")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        [Required(ErrorMessage = "Usted debe ingresar el campo {0}")]
        public string Description { get; set; }

        //Lado Muchos
        [JsonIgnore]
        public virtual ICollection<Employee> Employees { get; set; }

        [JsonIgnore]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}