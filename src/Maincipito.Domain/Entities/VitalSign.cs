using System;
using System.ComponentModel.DataAnnotations;

namespace Hospi.App.Domain.Entities
{
    [Display(Name = "Signo vital")]
    public class VitalSign
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage ="Debe proporcionar la fecha y hora de la medición")]
        public DateTime DateTime { get; set; }
        
        [Required(ErrorMessage = "Debe proporcionar un valor"), Display(Name = "Valor")]
        [RegularExpression("^[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)$", ErrorMessage = "El campo longitud debe ser un número")]
        public string Value { get; set; }

        [Display(Name = "Signo vital")]
        [EnumDataType(typeof(Sign))]
        public Sign Sign { get; set; }
    }
}
