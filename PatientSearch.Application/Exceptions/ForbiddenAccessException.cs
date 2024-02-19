namespace PatientSearch.Application.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public ForbiddenAccessException()
        {
        }
        public ForbiddenAccessException(string message, SystemException inner)
            : base(message, inner)
        {
        }
        public ForbiddenAccessException(String message, string errorCode, String errorDescription)
            : base(message)
        {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }
    }
}
