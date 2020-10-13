using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SchoolRepairSystem.IRepository;
using SchoolRepairSystem.IService;
using SchoolRepairSystemModels;

namespace SchoolRepairSystem.Service
{
    public class BaseService<T>:IBaseService<T> where T:BaseEntity,new()
    {
        public IBaseRepository<T> _BaseDal;

        public async Task<long> Add(T t)
        {
            return await _BaseDal.Add(t);
        }
        /// <summary>
        /// 返回插入了几条数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<int> Add(List<T> t)
        {
            return await _BaseDal.Add(t);
        }

        public async Task<bool> Delete(long id)
        {
            return await _BaseDal.Delete(id);
        }

        public async Task<bool> DeleteList(List<T> t)
        {
            return await _BaseDal.DeleteList(t);
        }

        public async Task<bool> DeleteList(List<long> t)
        {
            return await _BaseDal.DeleteList(t);
        }


        public async Task<T> QueryById(long id)
        {
            return await _BaseDal.QueryById(id);
        }

        public async Task<T> Query(Expression<Func<T, bool>> firstExpression)
        {
            return await _BaseDal.Query(firstExpression);
        }

        public async Task<List<T>> QueryList(List<long> t)
        {
            return await _BaseDal.QueryList(t);
        }

        public async Task<List<T>> QueryList(Expression<Func<T, bool>> whereExpression)
        {
            return await _BaseDal.QueryList(whereExpression);
        }

        public async Task<bool> Update(T t)
        {
            return await _BaseDal.Update(t);
        }

        public async Task<List<T>> QueryAll()
        {
            return await _BaseDal.QueryAll();
        }

        public async Task<List<T>> QueryPaging(int pageNum, int pageSize)
        {
            return await _BaseDal.QueryPaging(pageNum, pageSize);
        }


        

    }
}