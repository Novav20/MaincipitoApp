using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospi.App.Domain.Entities
{
    [Display(Name ="Objeto Médico")]
    public class Doctor : Person
    {
        [Required(ErrorMessage ="Se requiere una especialidad"), StringLength(50), Display(Name = "Especialidad")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$",ErrorMessage ="Solo se admiten letras")]
        public string MedicalSpecialty { get; set; }
        
        [Required(ErrorMessage ="Debe proporcionar un código"), StringLength(50), Display(Name = "Código")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Debe proporcionar el registro rethus"), StringLength(50), Display(Name = "Registro Rethus")]
        public string RethusRecord { get; set; }

        [Display(Name ="Pacientes a cargo")]
        public IList<Patient> Patients {get;set;}
    }

}
