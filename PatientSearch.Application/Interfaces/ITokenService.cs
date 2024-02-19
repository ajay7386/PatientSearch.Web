namespace PatientSearch.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateJwtSecurityToken(string emailId);

    }
}
