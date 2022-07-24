using System.ComponentModel.DataAnnotations;

namespace Hospi.App.Domain.Entities
{
    public enum Genre
    {
        [Display(Name = "Masculino")]
        MALE,

        [Display(Name = "Femenino")]
        FEMALE,

        [Display(Name = "Bisexual")]
        BISEXUAL,

        [Display(Name = "Intersexual")]
        INTERSEX,

        [Display(Name = "Pansexual")]
        PANSEXUAL,

        [Display(Name = "Transexual")]
        TRANSSEXUAL
    }
}