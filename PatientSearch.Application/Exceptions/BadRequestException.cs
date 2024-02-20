namespace PatientSearch.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public BadRequestException()
        {
        }
        public BadRequestException(string message, SystemException inner)
            : base(message, inner)
        {
        }
        public BadRequestException(String message, string errorCode, String errorDescription)
            : base(message)
        {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }
    }
}
