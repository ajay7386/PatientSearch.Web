namespace PatientSearch.Application.Request;

public class PatientRequest
{
    public string Query { get; set; }
    public int DepartmentId { get; set; }
    public string SortBy { get; set; }
    public int Page { get; set; } 
    public int PageSize { get; set; } 
}
