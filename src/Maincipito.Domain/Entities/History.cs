using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospi.App.Domain.Entities
{
    public class History
    {
        [Key]
        [Display(Name ="ID")]
        public int Id { get; set; }
        
        [Display(Name ="Sugerencias")]
        public IList<CareSuggestion> Suggestions { get; set; }
        
        //[StringLength(200)]
        [Display(Name ="Diagnostico")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public string Diagnosis { get; set; }
        
        //[StringLength(200)]
        [Display(Name ="Entorno")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public string Enviroment { get; set; }
    }

}

