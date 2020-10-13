using Microsoft.EntityFrameworkCore;
using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IRepository.UnitWork;
using SchoolRepairSystemModels;

namespace SchoolRepairSystem.Repository
{
    public class RoleWareHouseRepository:BaseRepository<RoleWareHouse>,IRoleWareHouseRepository
    {
        public RoleWareHouseRepository() : base(new UnitWork.UnitWork(new SchoolRepairSystemDbContext()))
        {

        }
        public RoleWareHouseRepository(IUnitWork unitWork) : base(unitWork)
        {
        }
    }
}