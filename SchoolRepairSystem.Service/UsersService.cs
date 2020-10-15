using System.Linq;
using System.Threading.Tasks;
using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IService;
using SchoolRepairSystem.Models;

namespace SchoolRepairSystem.Service
{
    public class UsersService:BaseService<Users>,IUsersService
    {
        private readonly IUsersRepository _dal;

        public UsersService(IUsersRepository dal) 
        {
            _dal = dal;
            base._BaseDal = dal;
        }

        public async Task<long> CheckAdd(string userName,string password,string mail="",string phone="")
        {
            return await _dal.CheckAdd(userName, password, mail, phone);

        }

    }
}