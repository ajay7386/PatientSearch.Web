using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PatientSearch.Application.Dto;
using PatientSearch.Application.Interfaces;
using PatientSearch.Application.Request;

namespace PatientSearch.Infrastructure.Repositories
{
    public class PatientRepo : IPatientRepo
    {
        private readonly PatientSearchDbContext _dbContext;
        private readonly ILogger<PatientRepo> _logger;

        public PatientRepo(PatientSearchDbContext dbContext, ILogger<PatientRepo> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<PatientDetails>> SearchPatients(PatientRequest patientRequest)
        {
            _logger.LogInformation("Start calling SearchPatients in Repositroy method!");

            List<PatientDetails> queryable = new List<PatientDetails>();
            queryable = await (from p in _dbContext.Patients
                               join d in _dbContext.Departments on p.DepartmentId equals d.Id into depttemp
                               from dept in depttemp.DefaultIfEmpty()
                               select new PatientDetails()
                               {
                                   PatientId = p.Id,
                                   Name = p.Name,
                                   AdminsionDate = p.AdmissionDate,
                                   Problem = p.Problem,
                                   Address = $"{p.Address1} {p.City} {p.State} {p.Country} {p.PostalCode}",
                                   DepartmentName = dept.DepartmentName,
                                   DepartmentId = dept.Id
                               }).ToListAsync();
            //// Apply filters
            if (!string.IsNullOrEmpty(patientRequest.Query))
            {
                queryable = queryable.Where(p => p.Address.ToUpper().Contains(patientRequest.Query.ToUpper()) || p.Name.ToUpper().Contains(patientRequest.Query.ToUpper())
                                            || p.Problem.ToUpper().Contains(patientRequest.Query.ToUpper())).ToList();
            }
            if (patientRequest.DepartmentId != 0)
            {
                queryable = queryable.Where(p => p.DepartmentId == patientRequest.DepartmentId).ToList();
            }

            //// Apply sorting
            switch (patientRequest?.SortBy?.ToLower())
            {
                case "name":
                    queryable = queryable.OrderBy(p => p.Name).ToList();
                    break;
                case "adminsiondate":
                    queryable = queryable.OrderBy(p => p.AdminsionDate).ToList();
                    break;
                case "department":
                    queryable = queryable.OrderBy(p => p.DepartmentName).ToList();
                    break;
                default:
                    // Default sorting logic
                    break;
            }
            ////Added pagigation Logic
            if (queryable.Count > 0)
            {
                queryable = queryable.Skip((patientRequest.Page - 1) * patientRequest.PageSize).Take(patientRequest.PageSize).ToList();
            }

            return queryable;
        }
    }
}
