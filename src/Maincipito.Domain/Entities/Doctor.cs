using System.ComponentModel.DataAnnotations;

namespace Maincipito.Domain.Entities;

[Display(Name = "Médico")]
public class Doctor : Person
{
    [Required(ErrorMessage = "Se requiere una especialidad")]
    [StringLength(50), Display(Name = "Especialidad")]
    [RegularExpression(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$", ErrorMessage = "Solo se admiten letras")]
    public string MedicalSpecialty { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe proporcionar un código")]
    [StringLength(50), Display(Name = "Código")]
    public string Code { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe proporcionar el registro Rethus")]
    [StringLength(50), Display(Name = "Registro Rethus")]
    public string RethusRecord { get; set; } = string.Empty;

    [Display(Name = "Pacientes a cargo")]
    public IList<Patient> Patients { get; set; } = new List<Patient>();
}