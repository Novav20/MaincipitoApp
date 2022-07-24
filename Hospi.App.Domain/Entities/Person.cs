using System.ComponentModel.DataAnnotations;

namespace Hospi.App.Domain.Entities
{
    public class Person
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre(s) es requerido")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$",ErrorMessage ="Solo se admiten letras")]
        [StringLength(50), Display(Name = "Nombre(s)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo apellido(s) es requerido")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$",ErrorMessage ="Solo se admiten letras")]
        [StringLength(50), Display(Name = "Apellido(s)")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Se requiere un número de télefono")]
        [RegularExpression("([1-9][0-9]*)",ErrorMessage = "Debe introducir el número sin espacios ni caracteres especiales")]
        [StringLength(50), Display(Name = "Teléfono")]
        public string Cellphone { get; set; }

        [Display(Name = "Género")]
        [EnumDataType(typeof(Genre))]
        public Genre Genre { get; set; }
    }
}

