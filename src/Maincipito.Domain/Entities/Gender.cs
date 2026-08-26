using System.ComponentModel.DataAnnotations;

namespace Maincipito.Domain.Entities;

public enum Gender
{
    [Display(Name = "Masculino")]
    Male,

    [Display(Name = "Femenino")]
    Female,

    [Display(Name = "Bisexual")]
    Bisexual,

    [Display(Name = "Intersexual")]
    Intersex,

    [Display(Name = "Pansexual")]
    Pansexual,

    [Display(Name = "Transexual")]
    Transgender,

    [Display(Name = "Otro")]
    Other
}