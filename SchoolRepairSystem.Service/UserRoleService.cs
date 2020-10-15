using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IService;
using SchoolRepairSystem.Models;

namespace SchoolRepairSystem.Service
{
    public class UserRoleService:BaseService<UserRole>,IUserRoleService
    {
        private readonly IUserRoleRepository _dal;

        public UserRoleService(IUserRoleRepository dal) 
        {
            _dal = dal;
            base._BaseDal = dal;
        }
        

    }
}