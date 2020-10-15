using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IService;
using SchoolRepairSystem.Models;

namespace SchoolRepairSystem.Service
{
    public class WareHouseService:BaseService<WareHouse>,IWareHouseService
    {
        private IWareHouseRepository _dal;

        public WareHouseService(IWareHouseRepository dal) 
        {
            _dal = dal;
            base._BaseDal = dal;
        }
    }
}