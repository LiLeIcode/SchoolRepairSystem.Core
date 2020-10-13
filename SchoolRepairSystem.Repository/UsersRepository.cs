using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IRepository.UnitWork;
using SchoolRepairSystemModels;

namespace SchoolRepairSystem.Repository
{
    public class UsersRepository:BaseRepository<Users>,IUsersRepository
    {
        public UsersRepository(IUnitWork unitWork):base(unitWork)
        {
            
        }
        public UsersRepository() : base(
            new UnitWork.UnitWork(
                new SchoolRepairSystemDbContext())){}


        public async Task<long> CheckAdd(string userName, string password, string mail = "", string phone = "")
        {
            
                Users user = base.Query(x => x.UserName.Equals(userName))?.Result;
                if (user == null)
                {
                    long id = await base.Add(new Users()
                    {
                        UserName = userName,
                        Password = password,
                        Phone = phone,
                        Mail = mail
                    });
                    return id;
                }

                return -1;
        }
    }
}