using FluentAssertions;
using Maincipito.Domain.Entities;

namespace Maincipito.Domain.Tests.Entities;

public class PatientTests
{
    [Fact]
    public void Patient_WhenInstantiated_ShouldInitializeCollectionsAndDefaults()
    {
        // Act
        var patient = new Patient();

        // Assert
        patient.VitalSigns.Should().NotBeNull();
        patient.VitalSigns.Should().BeEmpty();
        patient.Gender.Should().Be(Gender.Male);
        patient.Name.Should().BeEmpty();
        patient.Surname.Should().BeEmpty();
        patient.Address.Should().BeEmpty();
        patient.City.Should().BeEmpty();
    }

    [Fact]
    public void Patient_ShouldInheritFromPerson()
    {
        // Act
        var patient = new Patient();

        // Assert
        patient.Should().BeAssignableTo<Person>();
    }
}