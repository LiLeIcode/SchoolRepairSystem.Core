using Microsoft.EntityFrameworkCore;
using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IRepository.UnitWork;
using SchoolRepairSystemModels;

namespace SchoolRepairSystem.Repository
{
    public class RoleReportForRepairRepository:BaseRepository<RoleReportForRepair>,IRoleReportForRepairRepository
    {
        public RoleReportForRepairRepository() : base(new UnitWork.UnitWork(new SchoolRepairSystemDbContext()))
        {

        }
        public RoleReportForRepairRepository(IUnitWork unitWork) : base(unitWork)
        {
        }
    }
}