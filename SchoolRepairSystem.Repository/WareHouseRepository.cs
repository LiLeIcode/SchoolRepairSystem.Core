using Microsoft.EntityFrameworkCore;
using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IRepository.UnitWork;
using SchoolRepairSystem.Models;

namespace SchoolRepairSystem.Repository
{
    public class WareHouseRepository:BaseRepository<WareHouse>,IWareHouseRepository
    {
        public WareHouseRepository() : base(new UnitWork.UnitWork(new SchoolRepairSystemDbContext()))
        {

        }
        public WareHouseRepository(IUnitWork unitWork) : base(unitWork)
        {
        }
    }
}