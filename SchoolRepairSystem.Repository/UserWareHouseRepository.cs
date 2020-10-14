using Microsoft.EntityFrameworkCore;
using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IRepository.UnitWork;
using SchoolRepairSystemModels;

namespace SchoolRepairSystem.Repository
{
    public class UserWareHouseRepository:BaseRepository<UserWareHouse>,IUserWareHouseRepository
    {
        public UserWareHouseRepository() : base(new UnitWork.UnitWork(new SchoolRepairSystemDbContext()))
        {

        }
        public UserWareHouseRepository(IUnitWork unitWork) : base(unitWork)
        {
        }
    }
}