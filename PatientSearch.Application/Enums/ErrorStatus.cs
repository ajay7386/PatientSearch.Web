namespace PatientSearch.Application.Enums
{
    public class ErrorStatus
    {
        public enum ErrorCode
        {
            dataNotFound = 1,
            invalidateInput,
            systemError,
            generalError
        }
    }
}
