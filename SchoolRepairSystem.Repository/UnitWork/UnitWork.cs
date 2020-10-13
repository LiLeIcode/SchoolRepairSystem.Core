using System;
using Microsoft.EntityFrameworkCore;
using SchoolRepairSystem.IRepository.UnitWork;
using SchoolRepairSystemModels;

namespace SchoolRepairSystem.Repository.UnitWork
{
    public class UnitWork:IUnitWork
    {
        private readonly SchoolRepairSystemDbContext _db;

        public UnitWork(SchoolRepairSystemDbContext db)
        {
            _db = db;
            
        }

        public DbContext GetDbContext()
        {
            return _db as DbContext;
        }
    }
}