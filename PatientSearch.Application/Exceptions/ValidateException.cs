namespace PatientSearch.Application.Exceptions
{
    public class ValidateException : Exception
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public ValidateException()
        {
        }
        public ValidateException(string message, SystemException inner)
            : base(message, inner)
        {
        }
        public ValidateException(String message, string errorCode, String errorDescription)
            : base(message)
        {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }
    }
}
