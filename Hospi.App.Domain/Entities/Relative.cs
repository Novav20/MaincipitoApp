using System.ComponentModel.DataAnnotations;

namespace Hospi.App.Domain.Entities
{
    public class Relative : Person
    {
        [Required(ErrorMessage = "Debe proporcionar un parentesco")]
        [Display(Name = "Parentesco"), StringLength(50)]
        public string Relationship { get; set; }

        [Required(ErrorMessage ="Debe proporcionar un correo electrónico")]
        [StringLength(50), Display(Name ="Correo Electrónico")]
        [EmailAddress(ErrorMessage ="Debe proporcionar un formato correcto")]
        public string Email { get; set; }
    }
}