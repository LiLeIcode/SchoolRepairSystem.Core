using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IRepository.UnitWork;
using SchoolRepairSystem.Models;

namespace SchoolRepairSystem.Repository
{
    public class MenuRepository:BaseRepository<Menus>,IMenuRepository
    {
        public MenuRepository(IUnitWork unitWork) : base(unitWork)
        {
        }

        public MenuRepository():base(new UnitWork.UnitWork(new SchoolRepairSystemDbContext()))
        {
            
        }
    }
}