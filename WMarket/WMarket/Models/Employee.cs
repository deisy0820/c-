using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WMarket.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Display(Name="Primer Nombre")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener entre {1} y {2} caracteres",MinimumLength = 3)]
        [Required(ErrorMessage = "Usted debe Ingresar {0}")]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        [StringLength(30, ErrorMessage = "El campo {0} debe tener entre {1} y {2} caracteres", MinimumLength = 3)]
        [Required(ErrorMessage = "Usted debe Ingresar {0}")]
        public string LastName { get; set; }

        [Display(Name = "Salario")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Usted debe Ingresar {0}")]
        public decimal Salary { get; set; }

        [Display(Name = "Porcentaje de Bono")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public float BonusPercent { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Usted debe Ingresar {0}")]
        public DateTime DateofBirth { get; set; }

        [Display(Name = "Hora de Inicio")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Usted debe Ingresar {0}")]
        public DateTime StartTime { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [Display(Name = "Url")]
        [DataType(DataType.Url)]
        public string URL { get; set; }

        [Display(Name = "Documento")]
        [Required(ErrorMessage = "Usted debe Ingresar {0}")]
        public int DocumentTypeID { get; set; }

        [NotMapped]
        public int Age { get { return DateTime.Now.Year - DateofBirth.Year ;} }

        [JsonIgnore]
        public virtual DocumentType DocumentType { get; set; }
    }
}