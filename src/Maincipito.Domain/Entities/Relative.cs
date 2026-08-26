using System.ComponentModel.DataAnnotations;

namespace Maincipito.Domain.Entities;

[Display(Name = "Familiar")]
public class Relative : Person
{
    [Required(ErrorMessage = "Debe proporcionar un parentesco")]
    [StringLength(50), Display(Name = "Parentesco")]
    public string Relationship { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe proporcionar un correo electrónico")]
    [StringLength(50), Display(Name = "Correo Electrónico")]
    [EmailAddress(ErrorMessage = "Debe proporcionar un formato de correo válido")]
    public string Email { get; set; } = string.Empty;
}