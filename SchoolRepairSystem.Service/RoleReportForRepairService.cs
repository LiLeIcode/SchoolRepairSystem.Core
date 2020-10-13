using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IService;
using SchoolRepairSystemModels;

namespace SchoolRepairSystem.Service
{
    public class RoleReportForRepairService:BaseService<RoleReportForRepair>,IRoleReportForRepairService
    {
        private IRoleReportForRepairRepository _dal;

        public RoleReportForRepairService(IRoleReportForRepairRepository dal)
        {
            _dal = dal;
            base._BaseDal = dal;

        }
    }
}