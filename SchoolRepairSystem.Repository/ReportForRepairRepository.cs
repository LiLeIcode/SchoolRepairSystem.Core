using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IRepository.UnitWork;
using SchoolRepairSystem.Models;

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