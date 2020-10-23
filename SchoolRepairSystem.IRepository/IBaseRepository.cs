using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolRepairSystem.IRepository
{
    public interface IBaseRepository<T> where T:class
    {
        Task<long> Add(T t);
        Task<int> Add(List<T> t);
        Task<bool> Delete(long id);
        Task<bool> DeleteList(List<T> t);
        Task<bool> DeleteList(List<long> t);
        Task<T> QueryById(long id);
        Task<T> Query(Expression<Func<T, bool>> firstOrDefaultExpression);
        Task<List<T>> QueryList(List<long> t);
        Task<List<T>> QueryList(Expression<Func<T, bool>> whereExpression);

        Task<bool> Update(T t);

        Task<List<T>> QueryAll();

        Task<List<T>> QueryPaging(int pageNum,int pageSize);

        Task<List<T>> QueryPagingByExp(Expression<Func<T, bool>> whereExpression, int pageNum, int pageSize);

    }
}