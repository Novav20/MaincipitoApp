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
        public DateTime DateTime { get; set; }

        [StringLength(200)]
        [Display(Name ="Descripción")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public string Description { get; set; }
    }
}


