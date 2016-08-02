using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    public class Customer
    {
        [Key]
        [Display(Name = "Cliente")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public int CustomerID { get; set; }

        [Display(Name = "Primer Nombre")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        [Required(ErrorMessage = "Usted debe Ingresar {0}")]
        public string FirsName { get; set; }

        [Display(Name = "Apellido")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        [Required(ErrorMessage = "Usted debe Ingresar {0}")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        [Required(ErrorMessage = "Usted debe Ingresar {0}")]
        public string Phone { get; set; }

        [StringLength(30, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        [Required(ErrorMessage = "Usted debe Ingresar {0}")]
        public string Address { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Documento")]
        [StringLength(20, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 5)]
        [Required(ErrorMessage = "Usted debe Ingresar {0}")]
        public string Document { get; set; }

        [Display(Name = "Tipo Documento")]
        public int DocumentTypeID { get; set; }

        [NotMapped]//Para no guardar en base de dtos
        public string FullName { get { return string.Format("{0} {1}", FirsName, LastName) ;}  }

        //Lado UNO
        [JsonIgnore]
        public virtual DocumentType DocumentType { get; set; }

        //Lado Varios
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}