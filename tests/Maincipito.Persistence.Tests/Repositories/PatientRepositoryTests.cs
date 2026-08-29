using FluentAssertions;
using Maincipito.Domain.Entities;
using Maincipito.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Maincipito.Persistence.Tests.Repositories;

public class PatientRepositoryTests
{
    private static MaincipitoDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<MaincipitoDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Base de datos aislada por test
            .Options;

        return new MaincipitoDbContext(options);
    }

    [Fact]
    public async Task AddAsync_ShouldPersistPatientInDatabase()
    {
        // Arrange
        using var context = CreateDbContext();
        var repository = new PatientRepository(context);
        var patient = new Patient
        {
            Name = "Laura",
            Surname = "Quesada",
            Cellphone = "3119876543",
            City = "Cartagena",
            Address = "Calle 100 # 15-20",
            DateOfBirth = new DateTime(1999, 12, 31),
            Gender = Gender.Female
        };

        // Act
        var created = await repository.AddAsync(patient);

        // Assert
        created.Id.Should().BeGreaterThan(0);
        var savedPatient = await context.Patients.FindAsync(created.Id);
        savedPatient.Should().NotBeNull();
        savedPatient!.Name.Should().Be("Laura");
        savedPatient.Gender.Should().Be(Gender.Female);
    }

    [Fact]
    public async Task AssignDoctorAsync_ShouldLinkDoctorToPatient()
    {
        // Arrange
        using var context = CreateDbContext();
        var patient = new Patient
        {
            Name = "Pedro",
            Surname = "Alvarez",
            Cellphone = "3201112233",
            City = "Medellín",
            Address = "Carrera 45 # 10-10",
            DateOfBirth = new DateTime(1980, 1, 1),
            Gender = Gender.Male
        };
        var doctor = new Doctor
        {
            Name = "Dr. Andres",
            Surname = "Castro",
            Cellphone = "3009998877",
            Gender = Gender.Male,
            MedicalSpecialty = "Cardiología",
            Code = "DOC-001",
            RethusRecord = "RET-12345"
        };

        await context.Patients.AddAsync(patient);
        await context.Doctors.AddAsync(doctor);
        await context.SaveChangesAsync();

        var repository = new PatientRepository(context);

        // Act
        var result = await repository.AssignDoctorAsync(patient.Id, doctor.Id);

        // Assert
        result.Should().NotBeNull();
        var updatedPatient = await repository.GetWithDetailsAsync(patient.Id);
        updatedPatient!.Doctor.Should().NotBeNull();
        updatedPatient.Doctor!.Id.Should().Be(doctor.Id);
        updatedPatient.Doctor.MedicalSpecialty.Should().Be("Cardiología");
    }

    [Fact]
    public async Task GetAllAsync_WithSearchFilter_ShouldReturnMatchingPatients()
    {
        // Arrange
        using var context = CreateDbContext();
        await context.Patients.AddRangeAsync(
            new Patient { Name = "Maria", Surname = "Gomez", Cellphone = "3001", City = "Cali", Address = "A", DateOfBirth = DateTime.Today },
            new Patient { Name = "Mario", Surname = "Ruiz", Cellphone = "3002", City = "Cali", Address = "B", DateOfBirth = DateTime.Today },
            new Patient { Name = "Sofia", Surname = "Zapata", Cellphone = "3003", City = "Cali", Address = "C", DateOfBirth = DateTime.Today }
        );
        await context.SaveChangesAsync();

        var repository = new PatientRepository(context);

        // Act
        var results = await repository.GetAllAsync("Mar");

        // Assert
        results.Should().HaveCount(2);
        results.Should().OnlyContain(p => p.Name.StartsWith("Mar"));
    }
}