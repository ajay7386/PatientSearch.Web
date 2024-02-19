using FluentValidation;
using PatientSearch.Application.Request;

namespace PatientSearch.Web.Validation
{
    public class SearchPatientValidation : AbstractValidator<PatientRequest>
    {
        public SearchPatientValidation()
        {
            RuleFor(x => x.DepartmentId).InclusiveBetween(0, 100).WithMessage("Please Provide valide Department Id");
            RuleFor(x => x.Page).InclusiveBetween(1,100).WithMessage("Please enter valide page number!");
            RuleFor(x => x.PageSize).InclusiveBetween(1,200).WithMessage("Please enter valide page size!");
        }
    }
}
    