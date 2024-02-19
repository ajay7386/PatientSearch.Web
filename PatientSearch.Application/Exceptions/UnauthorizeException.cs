namespace PatientSearch.Application.Exceptions
{
    public class UnauthorizeException : Exception
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public UnauthorizeException()
        {
        }
        public UnauthorizeException(string message, SystemException inner)
            : base(message, inner)
        {
        }
        public UnauthorizeException(String message, string errorCode, String errorDescription)
            : base(message)
        {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }
    }
}
