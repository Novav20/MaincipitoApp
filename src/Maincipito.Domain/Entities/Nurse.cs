using System.ComponentModel.DataAnnotations;

namespace Maincipito.Domain.Entities;

[Display(Name = "Enfermera")]
public class Nurse : Person
{
    [Required(ErrorMessage = "El campo Tarjeta Profesional es requerido")]
    [StringLength(50), Display(Name = "Tarjeta Profesional")]
    public string ProfessionalCard { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe proporcionar un número de horas laborales")]
    [Display(Name = "Horas Laborales")]
    [Range(1, 168, ErrorMessage = "Las horas semanales deben estar entre 1 y 168")]
    public int WorkingHours { get; set; }
}