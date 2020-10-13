using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IService;
using SchoolRepairSystemModels;

namespace SchoolRepairSystem.Service
{
    public class RolesService:BaseService<Roles>,IRolesService
    {
        private IRolesRepository _dal;

        public RolesService(IRolesRepository dal) 
        {
            _dal = dal;
            base._BaseDal = dal;
        }
    }
}