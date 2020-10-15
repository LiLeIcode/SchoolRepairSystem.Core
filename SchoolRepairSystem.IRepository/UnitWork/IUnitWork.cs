using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SchoolRepairSystem.Models;

namespace SchoolRepairSystem.IRepository.UnitWork
{
    public interface IUnitWork
    {
        DbContext GetDbContext();
    }
}
