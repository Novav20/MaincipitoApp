using System.ComponentModel.DataAnnotations;

namespace Maincipito.Domain.Entities;

[Display(Name = "Signo Vital")]
public class VitalSign
{
    [Key]
    [Display(Name = "ID")]
    public int Id { get; set; }

    [Display(Name = "Fecha")]
    [Required(ErrorMessage = "Debe proporcionar la fecha y hora de la medición")]
    public DateTime DateTime { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "Debe proporcionar un valor")]
    [Display(Name = "Valor")]
    [RegularExpression(@"^[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)$", ErrorMessage = "El valor debe ser numérico")]
    public string Value { get; set; } = string.Empty;

    [Display(Name = "Signo vital")]
    [EnumDataType(typeof(Sign))]
    public Sign Sign { get; set; } = Sign.HeartRate;
}