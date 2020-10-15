using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IService;
using SchoolRepairSystem.Models;

namespace SchoolRepairSystem.Service
{
    public class ReportForRepairService:BaseService<ReportForRepair>, IReportForRepairService
    {
        private readonly IReportForRepairRepository _dal;

        public ReportForRepairService(IReportForRepairRepository dal)
        {
            _dal = dal;
            base._BaseDal = dal;
        }
    }
}