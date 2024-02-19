using Microsoft.Extensions.Logging;
using PatientSearch.Application.Dto;
using PatientSearch.Application.Interfaces;
using PatientSearch.Application.Request;


namespace PatientSearch.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepo _patientRepository;
        private readonly IUserManagementRepo _userManagementRepo;
        private readonly ILogger<PatientService> _logger;

        public PatientService(IPatientRepo patientRepository, IUserManagementRepo userManagementRepo, ILogger<PatientService> logger)
        {
            _patientRepository = patientRepository;
            _userManagementRepo = userManagementRepo;
            _logger = logger;
        }

        public async Task<IEnumerable<PatientDetails>> SearchPatients(PatientRequest patientRequest)
        {
            _logger.LogInformation("Patent Service calling to SearchPatients!");
            return await _patientRepository.SearchPatients(patientRequest);
        }

        public async Task<bool> ValidateUser(string username, string password)
        {
            _logger.LogInformation("Patent Service calling to SearchPatients!");
            return await _userManagementRepo.ValidateUser(username, password);
        }
    }
}
