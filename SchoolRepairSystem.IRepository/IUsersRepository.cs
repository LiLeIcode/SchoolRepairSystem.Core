using System.Threading.Tasks;
using SchoolRepairSystemModels;

namespace SchoolRepairSystem.IRepository
{
    public interface IUsersRepository : IBaseRepository<Users>
    {
        Task<long> CheckAdd(string userName, string password, string mail = "", string phone = "");
    }
}