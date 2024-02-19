using Microsoft.EntityFrameworkCore;
using PatientSearch.Domain.Entities;

namespace PatientSearch.Infrastructure
{
    public partial class PatientSearchDbContext : DbContext
    {
        public PatientSearchDbContext(DbContextOptions<PatientSearchDbContext> options) : base(options)
        {

        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<UserMaster> UserMasters { get; set; }
    }
}