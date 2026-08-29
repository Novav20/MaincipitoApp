using Maincipito.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Maincipito.Persistence;

public static class DbInitializer
{
    public static async Task SeedAsync(MaincipitoDbContext context)
    {
        // Si ya existen pacientes, no duplicar datos
        if (await context.Patients.AnyAsync())
        {
            return;
        }

        // 1. Personal Médico
        var doc1 = new Doctor
        {
            Name = "Valentina",
            Surname = "Restrepo Jaramillo",
            Cellphone = "3104567890",
            Gender = Gender.Female,
            MedicalSpecialty = "Medicina Interna",
            Code = "DOC-0104",
            RethusRecord = "RET-89421-ANT"
        };

        var doc2 = new Doctor
        {
            Name = "Carlos Eduardo",
            Surname = "Mendoza Silva",
            Cellphone = "3157891234",
            Gender = Gender.Male,
            MedicalSpecialty = "Cardiología Clínica",
            Code = "DOC-0205",
            RethusRecord = "RET-67319-BOG"
        };

        var doc3 = new Doctor
        {
            Name = "Andrés Felipe",
            Surname = "Morales Caicedo",
            Cellphone = "3123456789",
            Gender = Gender.Male,
            MedicalSpecialty = "Geriatría y Cuidado Paliativo",
            Code = "DOC-0308",
            RethusRecord = "RET-45120-VAL"
        };

        await context.Doctors.AddRangeAsync(doc1, doc2, doc3);

        // 2. Enfermeras
        var nurse1 = new Nurse
        {
            Name = "Claudia Marcela",
            Surname = "Quintero Gómez",
            Cellphone = "3206549870",
            Gender = Gender.Female,
            ProfessionalCard = "TP-98214-ENF",
            WorkingHours = 44
        };

        var nurse2 = new Nurse
        {
            Name = "David Alejandro",
            Surname = "Osorio Ríos",
            Cellphone = "3187412589",
            Gender = Gender.Male,
            ProfessionalCard = "TP-67123-ENF",
            WorkingHours = 40
        };

        await context.Nurses.AddRangeAsync(nurse1, nurse2);

        // 3. Familiares de Contacto
        var rel1 = new Relative
        {
            Name = "Gloria Esperanza",
            Surname = "Gómez de Valencia",
            Cellphone = "3169874561",
            Gender = Gender.Female,
            Relationship = "Cónyuge / Cuidador Principal",
            Email = "gloria.gomez@email.com"
        };

        var rel2 = new Relative
        {
            Name = "Fernando José",
            Surname = "Pineda Ortiz",
            Cellphone = "3118529630",
            Gender = Gender.Male,
            Relationship = "Hijo",
            Email = "f.pineda@email.com"
        };

        var rel3 = new Relative
        {
            Name = "Mariana Lucía",
            Surname = "Benítez Castro",
            Cellphone = "3147539512",
            Gender = Gender.Female,
            Relationship = "Hija / Tutora Legal",
            Email = "mariana.benitez@email.com"
        };

        await context.Relatives.AddRangeAsync(rel1, rel2, rel3);

        // 4. Historias Clínicas y Sugerencias de Cuidado
        var history1 = new History
        {
            Diagnosis = "Insuficiencia Cardíaca Congestiva (NYHA Clase II) / Hipertensión Arterial Grado 2",
            Environment = "Vivienda urbana primer piso, ventilación natural adecuada, paciente con cuidador permanente.",
            Suggestions = new List<CareSuggestion>
            {
                new() { DateTime = DateTime.UtcNow.AddDays(-5), Description = "Control estricto de ingesta de sodio (< 2g/día) y restricción de líquidos a 1.5L diarios." },
                new() { DateTime = DateTime.UtcNow.AddDays(-2), Description = "Pesaje diario en ayunas para monitoreo de retención hídrica. Alertar si aumento > 2kg en 48h." }
            }
        };

        var history2 = new History
        {
            Diagnosis = "Enfermedad Pulmonar Obstructiva Crónica (EPOC Gold II) / Diabetes Mellitus Tipo 2",
            Environment = "Habitación adaptada con concentrador de oxígeno a 2L/min, libre de humo y alérgenos.",
            Suggestions = new List<CareSuggestion>
            {
                new() { DateTime = DateTime.UtcNow.AddDays(-4), Description = "Terapia respiratoria asistida 2 veces al día y control de saturación de oxígeno post-esfuerzo." },
                new() { DateTime = DateTime.UtcNow.AddDays(-1), Description = "Monitoreo de glucometría preprandial y administración de insulina según esquema prescrito." }
            }
        };

        var history3 = new History
        {
            Diagnosis = "Accidente Cerebrovascular Isquémico en rehabilitación / Fibrilación Auricular",
            Environment = "Entorno adaptado con barras de apoyo, paciente en cama con colchón antiescaras.",
            Suggestions = new List<CareSuggestion>
            {
                new() { DateTime = DateTime.UtcNow.AddDays(-3), Description = "Cambios posturales cada 2 horas y ejercicios de movilización pasiva con fisioterapeuta." }
            }
        };

        await context.Histories.AddRangeAsync(history1, history2, history3);

        // 5. Pacientes (Vinculados a sus relaciones completas)
        var patient1 = new Patient
        {
            Name = "Sofía",
            Surname = "Valencia Arboleda",
            Cellphone = "3001234567",
            Gender = Gender.Female,
            Address = "Carrera 43A # 18 Sur - 125, Apto 502",
            City = "Medellín",
            DateOfBirth = new DateTime(1956, 4, 15),
            Latitude = "6.1985",
            Longitude = "-75.5721",
            Doctor = doc2,
            Nurse = nurse1,
            Relative = rel1,
            History = history1,
            VitalSigns = new List<VitalSign>
            {
                new() { DateTime = DateTime.UtcNow.AddHours(-24), Value = "135/85", Sign = Sign.BloodPressure },
                new() { DateTime = DateTime.UtcNow.AddHours(-24), Value = "76", Sign = Sign.HeartRate },
                new() { DateTime = DateTime.UtcNow.AddHours(-12), Value = "96", Sign = Sign.Oximetry },
                new() { DateTime = DateTime.UtcNow.AddHours(-12), Value = "36.5", Sign = Sign.Temperature },
                new() { DateTime = DateTime.UtcNow.AddHours(-2), Value = "128/80", Sign = Sign.BloodPressure },
                new() { DateTime = DateTime.UtcNow.AddHours(-2), Value = "72", Sign = Sign.HeartRate }
            }
        };

        var patient2 = new Patient
        {
            Name = "Jorge Alberto",
            Surname = "Pineda Restrepo",
            Cellphone = "3019876543",
            Gender = Gender.Male,
            Address = "Calle 52 # 45 - 28, Barrio El Porvenir",
            City = "Rionegro",
            DateOfBirth = new DateTime(1949, 11, 23),
            Latitude = "6.1542",
            Longitude = "-75.3741",
            Doctor = doc1,
            Nurse = nurse1,
            Relative = rel2,
            History = history2,
            VitalSigns = new List<VitalSign>
            {
                new() { DateTime = DateTime.UtcNow.AddHours(-18), Value = "91", Sign = Sign.Oximetry },
                new() { DateTime = DateTime.UtcNow.AddHours(-18), Value = "145", Sign = Sign.BloodGlucose },
                new() { DateTime = DateTime.UtcNow.AddHours(-6), Value = "93", Sign = Sign.Oximetry },
                new() { DateTime = DateTime.UtcNow.AddHours(-6), Value = "128", Sign = Sign.BloodGlucose },
                new() { DateTime = DateTime.UtcNow.AddHours(-1), Value = "37.0", Sign = Sign.Temperature }
            }
        };

        var patient3 = new Patient
        {
            Name = "Lucía Elena",
            Surname = "Castro Benítez",
            Cellphone = "3024561230",
            Gender = Gender.Female,
            Address = "Diagonal 75B # 32A - 10, Casa 3",
            City = "Medellín",
            DateOfBirth = new DateTime(1968, 8, 5),
            Latitude = "6.2312",
            Longitude = "-75.5912",
            Doctor = doc3,
            Nurse = nurse2,
            Relative = rel3,
            History = history3,
            VitalSigns = new List<VitalSign>
            {
                new() { DateTime = DateTime.UtcNow.AddHours(-10), Value = "120/75", Sign = Sign.BloodPressure },
                new() { DateTime = DateTime.UtcNow.AddHours(-10), Value = "84", Sign = Sign.HeartRate },
                new() { DateTime = DateTime.UtcNow.AddHours(-3), Value = "97", Sign = Sign.Oximetry },
                new() { DateTime = DateTime.UtcNow.AddHours(-3), Value = "36.7", Sign = Sign.Temperature }
            }
        };

        await context.Patients.AddRangeAsync(patient1, patient2, patient3);

        await context.SaveChangesAsync();
    }
}