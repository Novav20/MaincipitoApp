using System.ComponentModel.DataAnnotations;

namespace Maincipito.Domain.Entities;

[Display(Name = "Historia Clínica")]
public class History
{
    [Key]
    [Display(Name = "ID")]
    public int Id { get; set; }

    [Display(Name = "Sugerencias")]
    public IList<CareSuggestion> Suggestions { get; set; } = new List<CareSuggestion>();

    [Display(Name = "Diagnóstico")]
    [StringLength(200)]
    [RegularExpression(@"^[a-zA-ZÀ-ÿ0-9\u00f1\u00d1\s,.\-""'()]+$", ErrorMessage = "Formato de diagnóstico inválido")]
    public string Diagnosis { get; set; } = string.Empty;

    [Display(Name = "Entorno")]
    [StringLength(200)]
    [RegularExpression(@"^[a-zA-ZÀ-ÿ0-9\u00f1\u00d1\s,.\-""'()]+$", ErrorMessage = "Formato de entorno inválido")]
    public string Environment { get; set; } = string.Empty;
}