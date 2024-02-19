using PatientSearch.Application.Dto;
using PatientSearch.Application.Request;

namespace PatientSearch.Application.Interfaces;
public interface IPatientService
{
    Task<IEnumerable<PatientDetails>> SearchPatients(PatientRequest patientRequest);
    Task<bool> ValidateUser(string username, string password);
}

