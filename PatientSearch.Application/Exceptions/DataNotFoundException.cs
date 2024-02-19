namespace PatientSearch.Application.Exceptions;
public class DataNotFoundException : Exception
{
    public string ErrorCode { get; set; }
    public string ErrorDescription { get; set; }
    public DataNotFoundException()
    {

    }
    public DataNotFoundException(string message, SystemException inner)
        : base(message, inner)
    {

    }
    public DataNotFoundException(String message, string errorCode, String errorDescription)
        : base(message)
    {
        ErrorCode = errorCode;
        ErrorDescription = errorDescription;
    }
}
