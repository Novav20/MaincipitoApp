using System.ComponentModel.DataAnnotations;

namespace Maincipito.Domain.Entities;

public enum Sign
{
    [Display(Name = "Porcentaje de oxígeno en la sangre (Oximetría)")]
    Oximetry,

    [Display(Name = "Frecuencia respiratoria")]
    RespiratoryRate,

    [Display(Name = "Frecuencia cardíaca")]
    HeartRate,

    [Display(Name = "Temperatura corporal")]
    Temperature,

    [Display(Name = "Tensión arterial")]
    BloodPressure,

    [Display(Name = "Glucosa en la sangre")]
    BloodGlucose
}