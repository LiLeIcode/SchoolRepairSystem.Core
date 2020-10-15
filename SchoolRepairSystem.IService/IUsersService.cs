using System.Threading.Tasks;
using SchoolRepairSystem.Models;

namespace SchoolRepairSystem.IService
{
    public interface IUsersService : IBaseService<Users>
    {
        Task<long> CheckAdd(string userName, string password, string mail = "", string phone = "");
    }
}