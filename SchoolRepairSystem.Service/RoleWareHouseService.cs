using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IService;
using SchoolRepairSystemModels;

namespace SchoolRepairSystem.Service
{
    public class RoleWareHouseService:BaseService<RoleWareHouse>,IRoleWareHouseService
    {
        private IRoleWareHouseRepository _dal;

        public RoleWareHouseService(IRoleWareHouseRepository dal)
        {
            _dal = dal;
            base._BaseDal = dal;
        }
    }
}