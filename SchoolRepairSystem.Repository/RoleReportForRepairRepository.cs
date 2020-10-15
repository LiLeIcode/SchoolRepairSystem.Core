using Microsoft.EntityFrameworkCore;
using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IRepository.UnitWork;
using SchoolRepairSystem.Models;

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