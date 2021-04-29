using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IRepository.UnitWork;
using SchoolRepairSystem.Models;

namespace SchoolRepairSystem.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        private readonly IUnitWork _unitWork;
        private readonly DbContext _dbContext;
        //private readonly SchoolRepairSystemDbContext _dbContext;

        public BaseRepository(IUnitWork unitWork)
        {
            _unitWork = unitWork;
            _dbContext = _unitWork.GetDbContext();
        }

        //internal DbContext GetDbContext => _dbContext;

        public async Task<long> Add(T t)
        {
            EntityEntry<T> add = await _dbContext.Set<T>().AddAsync(t);
            await _dbContext.SaveChangesAsync();
            return add.Entity.Id;
        }

        public async Task<int> Add(List<T> t)
        {
            foreach (T entity in t)
            {
                await _dbContext.Set<T>().AddAsync(entity);
            }

            int saveChangesAsync = await _dbContext.SaveChangesAsync();
            return saveChangesAsync;
        }

        public async Task<bool> Delete(long id)
        {
            T byId = await this.QueryById(id);
            byId.IsRemove = true;
            _dbContext.Entry(byId).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return this.Query(x => x.IsRemove).Result.IsRemove;
        }

        public async Task<bool> DeleteList(List<T> t)
        {
            foreach (T baseEntity in t)
            {
                baseEntity.IsRemove = true;
                _dbContext.Entry(baseEntity).State = EntityState.Modified;
            }
            int changesAsync = await _dbContext.SaveChangesAsync();
            return changesAsync == t.Count;
        }

        public async Task<bool> DeleteList(List<long> t)
        {
            List<T> list = await this.QueryList(t);
            bool deleteList = await this.DeleteList(list);
            return deleteList;
        }


        public async Task<T> QueryById(long id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id && !x.IsRemove);
        }

        public async Task<T> Query(Expression<Func<T, bool>> firstOrDefaultExpression)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(firstOrDefaultExpression);
        }

        public async Task<List<T>> QueryList(List<long> t)
        {
            List<T> list = new List<T>();
            foreach (long id in t)
            {
                var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id&&!x.IsRemove);
                list.Add(entity);
            }

            return list;
        }

        public async Task<List<T>> QueryList(Expression<Func<T, bool>> whereExpression)
        {
            return await _dbContext.Set<T>().Where(whereExpression).ToListAsync();
        }

        public async Task<bool> Update(T t)
        {
            _dbContext.Entry(t).State = EntityState.Modified;
            int saveChangesAsync = await _dbContext.SaveChangesAsync();
            return saveChangesAsync > 0;
        }

        public async Task<List<T>> QueryAll()
        {
            return await _dbContext.Set<T>().Where(x => !x.IsRemove).ToListAsync();
        }

        public async Task<List<T>> QueryPaging(int pageNum, int pageSize)
        {
            //return await _dbContext.Set<T>().Skip(pageSize * (pageNum - 1)).Take(pageSize).Where(x => !x.IsRemove).ToListAsync();

            return await _dbContext.Set<T>().Where(x => !x.IsRemove).Skip(pageSize * (pageNum - 1)).Take(pageSize).Where(x => !x.IsRemove).ToListAsync();
        }
        public async Task<List<T>> QueryPagingByExp(Expression<Func<T,bool>> whereExpression,int pageNum, int pageSize)
        {
            //return await _dbContext.Set<T>().Skip(pageSize * (pageNum - 1)).Take(pageSize).Where(x => !x.IsRemove).ToListAsync();

            return await _dbContext.Set<T>().Where(whereExpression).Skip(pageSize * (pageNum - 1)).Take(pageSize).Where(x => !x.IsRemove).ToListAsync();
        }
    }
}