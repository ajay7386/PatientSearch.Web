using Microsoft.Extensions.Logging;
using Moq;
using PatientSearch.Application.Dto;
using PatientSearch.Application.Interfaces;
using PatientSearch.Application.Request;
using PatientSearch.Application.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PatientSearchApi.Tests
{
    public class PatientServiceTests
    {
        private readonly Mock<IPatientRepo> mockPatientRepo;
        private readonly Mock<IUserManagementRepo> mockUserManagementRepo;
        private readonly PatientService patientService;
        private readonly Mock<ILogger<PatientService>> mockLogger;
        public PatientServiceTests()
        {
            mockPatientRepo = new Mock<IPatientRepo>();
            mockUserManagementRepo = new Mock<IUserManagementRepo>();
            mockLogger = new Mock<ILogger<PatientService>>();
            patientService = new PatientService(mockPatientRepo.Object, mockUserManagementRepo.Object, mockLogger.Object);
        }
        [Fact]
        public async Task SearchPatients_ReturnsExpectedResultsAsync()
        {
            // Arrange
            var patientRequest = new PatientRequest
            {
                // Set up your patient request object
                Query = String.Empty,
                DepartmentId = 0,
                SortBy = "Name",
                Page = 1,
                PageSize = 10
            };

            var expectedResults = new List<PatientDetails>
             {
            new PatientDetails {PatientId=1,  Name = "John Doe", Problem="Cold and Fever", Address="Test address1"
                , DepartmentId = 2,
                DepartmentName="Operating theatre (OT)",
                AdminsionDate = new DateTime(2022, 1, 15)  },
            new PatientDetails { PatientId=2,  Name = "Doe John", Problem="Bacl pain", Address="Test address2"
                , DepartmentId = 3,
                DepartmentName="Intensive care unit (ICU)",
                AdminsionDate = new DateTime(2022, 1, 15) }
            };

            mockPatientRepo.Setup(repo => repo.SearchPatients(patientRequest))
                           .ReturnsAsync(expectedResults);

            // Act
            var result = await patientService.SearchPatients(patientRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResults, result);
        }

        [Fact]
        public async Task ValidateUser_ReturnsTrueForValidUser()
        {
            // Arrange          
            var username = "validUser";
            var password = "validPassword";

            mockUserManagementRepo.Setup(repo => repo.ValidateUser(username, password))
                                  .ReturnsAsync(true);

            // Act
            var result = await patientService.ValidateUser(username, password);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public async Task ValidateUser_ReturnsFalseForInvalidUser()
        {
            // Arrange
            var username = "invalidUser";
            var password = "invalidPassword";

            mockUserManagementRepo.Setup(repo => repo.ValidateUser(username, password))
                                  .ReturnsAsync(false);

            // Act
            var result = await patientService.ValidateUser(username, password);

            // Assert
            Assert.False(result);
        }
    }
}