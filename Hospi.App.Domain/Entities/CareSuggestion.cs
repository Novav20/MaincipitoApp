using System;
using System.ComponentModel.DataAnnotations;

namespace Hospi.App.Domain.Entities
{
    [Display(Name ="Sugerencia de cuidado")]
    public class CareSuggestion
    {
        [Key]
        [Display(Name ="ID")]
        public int Id { get; set; }

        [Display(Name ="Fecha de la sugerencia")]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage ="Se requiere una descripción de la sugerencia")]
        [StringLength(200)]
        [Display(Name ="Descripción")]
        public string Description { get; set; }
    }
}


