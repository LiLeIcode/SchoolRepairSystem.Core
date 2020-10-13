using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IService;
using SchoolRepairSystemModels;

namespace SchoolRepairSystem.Service
{
    public class ReportForRepairService:BaseService<ReportForRepair>, IReportForRepairService
    {
        private IReportForRepairRepository _dal;

        public ReportForRepairService(IReportForRepairRepository dal)
        {
            _dal = dal;
            base._BaseDal = dal;
        }
    }
}