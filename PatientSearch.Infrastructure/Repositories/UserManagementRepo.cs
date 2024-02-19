using Microsoft.EntityFrameworkCore;
using PatientSearch.Application.Interfaces;

namespace PatientSearch.Infrastructure.Repositories
{
    public class UserManagementRepo : IUserManagementRepo
    {
        private readonly PatientSearchDbContext _dbContext;

        public UserManagementRepo(PatientSearchDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> ValidateUser(string username, string password)
        {
            bool isExist = false;
            var userMaster = await _dbContext.UserMasters.Where(x => x.UserName == username && x.Password == password && x.IsActive == true).ToListAsync();

            if (userMaster.Count != 0)
            {
                isExist = true;
            }
            return isExist;
        }
    }
}
