namespace PatientSearch.Application.Exceptions
{
    public class InvalidInputException : Exception
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public InvalidInputException()
        {
        }
        public InvalidInputException(string message, SystemException inner)
            : base(message, inner)
        {
        }

        public InvalidInputException(String message, string errorCode, String errorDescription)
            : base(message)
        {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }
    }
}
