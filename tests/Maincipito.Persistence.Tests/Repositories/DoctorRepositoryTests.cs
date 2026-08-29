using FluentAssertions;
using Maincipito.Domain.Entities;
using Maincipito.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Maincipito.Persistence.Tests.Repositories;

public class DoctorRepositoryTests
{
    private static MaincipitoDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<MaincipitoDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new MaincipitoDbContext(options);
    }

    [Fact]
    public async Task GetAllAsync_WithSpecialtySearch_ShouldReturnFilteredDoctors()
    {
        // Arrange
        using var context = CreateDbContext();
        await context.Doctors.AddRangeAsync(
            new Doctor { Name = "Alberto", Surname = "Plata", Cellphone = "3111", Code = "D1", RethusRecord = "R1", MedicalSpecialty = "Pediatría" },
            new Doctor { Name = "Beatriz", Surname = "Mora", Cellphone = "3112", Code = "D2", RethusRecord = "R2", MedicalSpecialty = "Dermatología" },
            new Doctor { Name = "Camila", Surname = "Perez", Cellphone = "3113", Code = "D3", RethusRecord = "R3", MedicalSpecialty = "Pediatría" }
        );
        await context.SaveChangesAsync();

        var repository = new DoctorRepository(context);

        // Act
        var pediatricians = await repository.GetAllAsync("Pediatría");

        // Assert
        pediatricians.Should().HaveCount(2);
        pediatricians.Should().OnlyContain(d => d.MedicalSpecialty == "Pediatría");
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveDoctorFromDatabase()
    {
        // Arrange
        using var context = CreateDbContext();
        var doctor = new Doctor
        {
            Name = "Ernesto",
            Surname = "Rios",
            Cellphone = "3114",
            Code = "D4",
            RethusRecord = "R4",
            MedicalSpecialty = "Neurología"
        };
        await context.Doctors.AddAsync(doctor);
        await context.SaveChangesAsync();

        var repository = new DoctorRepository(context);

        // Act
        await repository.DeleteAsync(doctor.Id);

        // Assert
        var exists = await repository.ExistsAsync(doctor.Id);
        exists.Should().BeFalse();
    }
}