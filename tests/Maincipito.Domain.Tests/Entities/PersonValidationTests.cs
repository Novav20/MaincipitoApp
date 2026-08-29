using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Maincipito.Domain.Entities;

namespace Maincipito.Domain.Tests.Entities;

public class PersonValidationTests
{
    private static List<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, context, validationResults, true);
        return validationResults;
    }

    [Fact]
    public void Person_WithValidData_ShouldPassValidation()
    {
        // Arrange
        var person = new Person
        {
            Name = "Carlos",
            Surname = "Quesada",
            Cellphone = "3001234567",
            Gender = Gender.Male
        };

        // Act
        var results = ValidateModel(person);

        // Assert
        results.Should().BeEmpty();
    }

    [Theory]
    [InlineData("Carlos123", "Gomez", "3001234567")] // Nombre con números
    [InlineData("Carlos", "Gomez!", "3001234567")]   // Apellido con caracteres especiales
    [InlineData("Carlos", "Gomez", "abc")]           // Teléfono no numérico
    public void Person_WithInvalidData_ShouldFailValidation(string name, string surname, string cellphone)
    {
        // Arrange
        var person = new Person
        {
            Name = name,
            Surname = surname,
            Cellphone = cellphone,
            Gender = Gender.Male
        };

        // Act
        var results = ValidateModel(person);

        // Assert
        results.Should().NotBeEmpty();
    }
}