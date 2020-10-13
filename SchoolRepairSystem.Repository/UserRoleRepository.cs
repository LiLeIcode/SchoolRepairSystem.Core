using Microsoft.EntityFrameworkCore;
using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IRepository.UnitWork;
using SchoolRepairSystemModels;

namespace SchoolRepairSystem.Repository
{
    public class UserRoleRepository:BaseRepository<UserRole>,IUserRoleRepository
    {
        public UserRoleRepository() : base(new UnitWork.UnitWork(new SchoolRepairSystemDbContext()))
        {

        }
        public UserRoleRepository(IUnitWork unitWork) : base(unitWork)
        {
        }
    }
}