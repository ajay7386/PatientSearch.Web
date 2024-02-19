using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PatientSearch.Application.Dto;
using PatientSearch.Application.Interfaces;
using PatientSearch.Application.Request;
using PatientSearch.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PatientSearchApi.Tests
{
    public class PatientControllerTests
    {

        [Fact]
        public async Task SearchPatients_ReturnsOkResult()
        {
            // Arrange
            var mockPatientService = new Mock<IPatientService>();
            var mockLogger = new Mock<ILogger<PatientController>>();

            var controller = new PatientController(mockPatientService.Object, mockLogger.Object);


            var patientRequest = new PatientRequest
            {
                // Set up your patient request object
                Query = String.Empty,
                DepartmentId = 0,
                SortBy = "Name",
                Page = 1,
                PageSize = 10
            };

            var expectedResults = new PatientDetails
            {
                // Set up patient details properties
                PatientId = 1,
                Name = "John Doe",
                Problem = "Cold and Fever",
                Address = "Test address1",
                DepartmentId = 2,
                DepartmentName = "Operating theatre (OT)",
                AdminsionDate = new DateTime(2022, 1, 15)

            };

            mockPatientService.Setup(service => service.SearchPatients(patientRequest))
                   .ReturnsAsync(new List<PatientDetails> { expectedResults });

            // Act
            var result = await controller.SearchPatients(patientRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
