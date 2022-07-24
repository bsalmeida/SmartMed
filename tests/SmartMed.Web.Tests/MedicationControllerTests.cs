using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SmartMed.Controllers;
using SmartMed.Data;
using SmartMed.Domain;
using SmartMed.Web.DTO;
using Xunit;

namespace SmartMed.Web.Tests
{
    public class MedicationControllerTests
    {
        private readonly Fixture fixture;
        private readonly Mock<ILogger<MedicationController>> loggerMock;
        private readonly Mock<IMedicationRepository> medicationRepositoryMock;
        private readonly MedicationController controller;

        public MedicationControllerTests()
        {
            fixture = new Fixture();
            this.loggerMock = new Mock<ILogger<MedicationController>>();
            this.medicationRepositoryMock = new Mock<IMedicationRepository>();
            controller = new MedicationController(this.loggerMock.Object, this.medicationRepositoryMock.Object);
        }

        [Fact]
        public async Task OnGetAllMedicationsAsync_ShouldReturnExpectedResult()
        {
            // Arrange
            var medications = fixture.CreateMany<Medication>();

            this.medicationRepositoryMock
                .Setup(x => x.GetAllMedicationAsync())
                .ReturnsAsync(medications);

            // Act
            var result = await controller.GetAllMedicationsAsync();


            // Assert
            result
                .Should()
                .BeOfType<OkObjectResult>()
                .Which
                .Value
                .Should()
                .Be(medications);
        }

        [Fact]
        public async Task OnCreateMedicationAsync_ShouldReturnExpectedResult()
        {
            // Arrange
            var medicationDto = fixture.Create<MedicationDto>();
            var medication = fixture
                .Build<Medication>()
                .With(m => m.Name, medicationDto.Name)
                .With(m => m.Quantity, medicationDto.Quantity)
                .Create();

            this.medicationRepositoryMock
                .Setup(x => x.CreateMedicationAsync(It.IsAny<Medication>()))
                .ReturnsAsync(medication);

            // Act
            var result = await controller.CreateMedicationAsync(medicationDto);

            // Assert
            result
                .Should()
                .BeOfType<CreatedResult>()
                .Which
                .Value
                .Should()
                .Be(medication);
        }

        [Fact]
        public async Task OnCreateMedicationAsync_WithInvalidQuantity_ShouldReturnExpectedResult()
        {
            // Arrange
            var medicationDto = fixture
                .Build<MedicationDto>()
                .With(m => m.Quantity, 0)
                .Create();


            // Act
            var result = await controller.CreateMedicationAsync(medicationDto);

            // Assert
            result
                 .Should()
                 .BeOfType<BadRequestObjectResult>()
                 .Which
                 .Value
                 .Should()
                 .Be("Quantity must be greater than zero.");
        }

        [Fact]
        public async Task OnDeleteMedicationAsync_ShouldReturnExpectedResult()
        {
            // Arrange
            var medication = fixture.Create<Medication>();

            this.medicationRepositoryMock
                .Setup(x => x.DeleteMedicationAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var result = await controller.DeleteMedicationAsync(medication.Name);

            // Assert
            result
                .Should()
                .BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task OnDeleteMedicationAsync_WithInvalidName_ShouldReturnExpectedResult()
        {
            // Arrange
            var invalidName = fixture.Create<string>();

            this.medicationRepositoryMock
                .Setup(x => x.DeleteMedicationAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var result = await controller.DeleteMedicationAsync(invalidName);

            // Assert
            result
                 .Should()
                 .BeOfType<BadRequestObjectResult>()
                 .Which
                 .Value
                 .Should()
                 .Be($"Cannot delete medication with name {invalidName}.");
        }
    }
}
