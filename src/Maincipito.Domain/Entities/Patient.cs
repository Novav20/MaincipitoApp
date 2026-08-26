using System.ComponentModel.DataAnnotations;

namespace Maincipito.Domain.Entities;

[Display(Name = "Paciente")]
public class Patient : Person
{
    [Display(Name = "Historia Clínica")]
    public History? History { get; set; }

    [Display(Name = "Signos Vitales")]
    public IList<VitalSign> VitalSigns { get; set; } = new List<VitalSign>();

    [Display(Name = "Familiar")]
    public Relative? Relative { get; set; }

    [Display(Name = "Enfermera asignada")]
    public Nurse? Nurse { get; set; }

    [Display(Name = "Médico asignado")]
    public Doctor? Doctor { get; set; }

    [Required(ErrorMessage = "Se requiere una dirección de residencia")]
    [StringLength(100), Display(Name = "Dirección de residencia")]
    public string Address { get; set; } = string.Empty;

    [Display(Name = "Latitud")]
    [RegularExpression(@"^[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)$", ErrorMessage = "El campo latitud debe ser numérico")]
    public string? Latitude { get; set; }

    [Display(Name = "Longitud")]
    [RegularExpression(@"^[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)$", ErrorMessage = "El campo longitud debe ser numérico")]
    public string? Longitude { get; set; }

    [Required(ErrorMessage = "El campo ciudad es requerido")]
    [StringLength(50), Display(Name = "Ciudad")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe proporcionar una fecha de nacimiento")]
    [DataType(DataType.Date), Display(Name = "Fecha de Nacimiento")]
    public DateTime DateOfBirth { get; set; }
}