using System.ComponentModel.DataAnnotations;

namespace Hospi.App.Domain.Entities
{
    public class Nurse : Person
    {
        [Required(ErrorMessage ="El campo Tarjeta Profesional es requerido"), StringLength(50)]
        [Display(Name = "Tarjeta Profesional")]
        public string ProfessionalCard { get; set; }

        [Required(ErrorMessage ="Debe proporcionar un número de horas laborales")]
        public int WorkingHours { get; set; }
    }
}