using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospi.App.Domain.Entities
{
    [Display(Name = "Paciente")]
    public class Patient : Person
    {
        public History History { get; set; }
        public List<VitalSign> VitalSigns { get; set; }
        
        [Display(Name ="Familiar")]
        public Relative Relative { get; set; }
        public Nurse Nurse { get; set; }

        [Display(Name = "Médico asignado")]
        public Doctor Doctor { get; set; }

        [Required(ErrorMessage = "Se requiere una dirección de residencia")]
        [StringLength(100), Display(Name = "Dirección de residencia")]
        public string Address { get; set; }

        [Display(Name = "Latitud")]
        [RegularExpression("^[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)$", ErrorMessage = "El campo longitud debe ser un número")]
        public string Latitude { get; set; }

        [Display(Name = "Longitud")]
        [RegularExpression("^[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)$", ErrorMessage = "El campo longitud debe ser un número")]
        public string Longitude { get; set; }

        [Required(ErrorMessage = "El campo ciudad es requerido")]
        [StringLength(50), Display(Name = "Ciudad")]
        public string City { get; set; }

        [Required(ErrorMessage = "Debe proporcionar una fecha de nacimiento")]
        [DataType(DataType.Date), Display(Name = "Fecha de Nacimiento")]
        public DateTime DateOfBirth { get; set; }
    }

}

