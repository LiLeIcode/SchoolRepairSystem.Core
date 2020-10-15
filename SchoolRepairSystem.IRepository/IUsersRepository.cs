using System.Threading.Tasks;
using SchoolRepairSystem.Models;

namespace SchoolRepairSystem.IRepository
{
    public interface IUsersRepository : IBaseRepository<Users>
    {
        Task<long> CheckAdd(string userName, string password, string mail = "", string phone = "");
    }
}