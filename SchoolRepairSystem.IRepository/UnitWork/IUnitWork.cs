using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SchoolRepairSystemModels;

namespace SchoolRepairSystem.IRepository.UnitWork
{
    public interface IUnitWork
    {
        DbContext GetDbContext();
    }
}
