using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IService;
using SchoolRepairSystem.Models;

namespace SchoolRepairSystem.Service
{
    public class RoleReportForRepairService:BaseService<RoleReportForRepair>,IRoleReportForRepairService
    {
        private readonly IRoleReportForRepairRepository _dal;

        public RoleReportForRepairService(IRoleReportForRepairRepository dal)
        {
            _dal = dal;
            base._BaseDal = dal;

        }
    }
}