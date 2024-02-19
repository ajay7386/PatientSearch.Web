using PatientSearch.Application.Dto;
using PatientSearch.Application.Request;

namespace PatientSearch.Application.Interfaces
{
    public interface IPatientRepo
    {
        Task<IEnumerable<PatientDetails>> SearchPatients(PatientRequest patientRequest);
    }
}
