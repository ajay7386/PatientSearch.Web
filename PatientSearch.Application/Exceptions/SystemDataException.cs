namespace PatientSearch.Application.Exceptions
{
    public class SystemDataException : Exception
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public SystemDataException()
        {
        }
        public SystemDataException(string message, SystemException inner)
            : base(message, inner)
        {
        }

        public SystemDataException(String message, string errorCode, String errorDescription)
            : base(message)
        {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }
    }
}
