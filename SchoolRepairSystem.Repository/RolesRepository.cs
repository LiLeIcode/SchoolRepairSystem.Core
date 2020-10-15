using Microsoft.EntityFrameworkCore;
using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IRepository.UnitWork;
using SchoolRepairSystem.Models;

namespace SchoolRepairSystem.Repository
{
    public class RolesRepository:BaseRepository<Roles>,IRolesRepository
    {
        public RolesRepository() : base(new UnitWork.UnitWork(new SchoolRepairSystemDbContext()))
        {

        }
        public RolesRepository(IUnitWork unitWork) : base(unitWork)
        {
        }
    }
}