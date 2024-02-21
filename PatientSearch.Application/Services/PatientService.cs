using Microsoft.Extensions.Logging;
using PatientSearch.Application.Dto;
using PatientSearch.Application.Enums;
using PatientSearch.Application.Exceptions;
using PatientSearch.Application.Interfaces;
using PatientSearch.Application.Request;
using static PatientSearch.Application.Enums.ErrorStatus;

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
            try
            {
                if (patientRequest.Query == null)
                    throw new DataNotFoundException("No Data for serch Query!", ErrorCode.dataNotFound.ToString(), "No Data for serch Query!");

                return await _patientRepository.SearchPatients(patientRequest);
            }
            catch (InvalidInputException ex)
            {
                throw new InvalidInputException("Error while getting data from DB", ErrorStatus.ErrorCode.invalidateInput.ToString(), "Please provide valide input");
            }
            catch (SystemDataException ex)
            {
                throw new SystemDataException("Error while getting data from DB", ErrorStatus.ErrorCode.systemError.ToString(), "Please provide valide input");
            }
            catch (BadRequestException ex)
            {
                throw new BadRequestException("Error while getting data from DB", ErrorStatus.ErrorCode.badrequest.ToString(), ex.Message);
            }
            catch (Exception ex)
            {
                throw new DataNotFoundException("Error while getting data from DB", ErrorStatus.ErrorCode.generalError.ToString(), ex.Message);
            }
        }

        public async Task<bool> ValidateUser(string username, string password)
        {
            _logger.LogInformation("Patent Service calling to SearchPatients!");
            try
            {
                return await _userManagementRepo.ValidateUser(username, password);
            }
            catch (InvalidInputException ex)
            {
                throw new InvalidInputException("Error while getting data from DB", ErrorStatus.ErrorCode.invalidateInput.ToString(), "Please provide valide input");
            }
            catch (SystemDataException ex)
            {
                throw new SystemDataException("Error while getting data from DB", ErrorStatus.ErrorCode.systemError.ToString(), "Please provide valide input");
            }
            catch (BadRequestException ex)
            {
                throw new BadRequestException("Error while getting data from DB", ErrorStatus.ErrorCode.badrequest.ToString(), ex.Message);
            }
            catch (Exception ex)
            {
                throw new DataNotFoundException("Error while getting data from DB", ErrorStatus.ErrorCode.generalError.ToString(), ex.Message);
            }
        }
    }
}
