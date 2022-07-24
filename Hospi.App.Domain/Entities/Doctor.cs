using System.ComponentModel.DataAnnotations;

namespace Hospi.App.Domain.Entities
{
    [Display(Name ="Médico")]
    public class Doctor : Person
    {
        [Required, StringLength(50), Display(Name = "Especialidad")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$",ErrorMessage ="aaa")]
        public string MedicalSpecialty { get; set; }

        [Required, StringLength(50), Display(Name = "Código")]
        public string Code { get; set; }

        [Required, StringLength(50), Display(Name = "Registro Rethus")]
        public string RethusRecord { get; set; }
    }

}
