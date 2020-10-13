using Microsoft.EntityFrameworkCore;
using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IRepository.UnitWork;
using SchoolRepairSystemModels;

namespace SchoolRepairSystem.Repository
{
    public class ReportForRepairRepository:BaseRepository<ReportForRepair>, IReportForRepairRepository
    {
        public ReportForRepairRepository() : base(new UnitWork.UnitWork(new SchoolRepairSystemDbContext()))
        {

        }
        public ReportForRepairRepository(IUnitWork unitWork) : base(unitWork)
        {
        }
    }
}