using System.ComponentModel.DataAnnotations;

namespace Hospi.App.Domain.Entities
{
    public enum Sign
    {
        [Display(Name="porcentaje de oxígeno en la sangre")]
        OXIMETRY,

        [Display(Name="Frecuencia respiratoria")]
        RESPIRATORY_RATE,

        [Display(Name="Frecuencia Cardiáca")]
        HEART_RATE,

        [Display(Name="Temperatura Corporal")]
        TEMPERATURE,

        [Display(Name="Tensión Arterial")]
        BLOOD_PRESSURE,

        [Display(Name="Glucosa en la sangre")]
        BLOOD_GLUCOSE
    }
}