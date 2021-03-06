using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IService;
using SchoolRepairSystem.Models;

namespace SchoolRepairSystem.Service
{
    public class UserWareHouseService:BaseService<UserWareHouse>,IUserWareHouseService
    {
        private IUserWareHouseRepository _dal;

        public UserWareHouseService(IUserWareHouseRepository dal)
        {
            _dal = dal;
            base._BaseDal = dal;
        }
    }
}