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
        
        [Display(Name ="Fecha en que fue registrado el signo vital")]
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
        public Sign Sign { get; set; }
    }
}
