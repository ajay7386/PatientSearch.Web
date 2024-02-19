using System.ComponentModel.DataAnnotations;

namespace PatientSearch.Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = String.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required, MaxLength(100)]
        public string Problem { get; set; } = String.Empty;
        public DateTime AdmissionDate { get; set; }
        [Required, MaxLength(100)]
        public string Address1 { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Address2 { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string City { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string State { get; set; } = string.Empty;
        [Required, MaxLength(6)]
        public string PostalCode { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Country { get; set; } = string.Empty;
        public DateTime LastUpdate { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }

    public class Department
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string DepartmentName { get; set; } = string.Empty;
        public ICollection<Patient> Patient { get; set; }
    }
    public class UserMaster
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string UserName { get; set; }
        [Required, MaxLength(100)]
        public string Password { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
