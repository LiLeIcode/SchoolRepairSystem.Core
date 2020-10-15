using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IService;
using SchoolRepairSystem.Models;

namespace SchoolRepairSystem.Service
{
    public class MenuService:BaseService<Menus>,IMenuService
    {
        private readonly IMenuRepository _dal;

        public MenuService(IMenuRepository dal)
        {
            _dal = dal;
            base._BaseDal = dal;
        }
    }
}