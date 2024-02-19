namespace PatientSearch.Application.Interfaces;
public interface IUserManagementRepo
{
    Task<bool> ValidateUser(string username, string password);
}
