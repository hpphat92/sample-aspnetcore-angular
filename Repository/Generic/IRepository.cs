using NewApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NewApp.Repository.Generic
{
    public interface IRepository<T> where T : IBaseEntity
    {
        Task<List<T>> GetAll();
        Task<T> Get(string id);
        Task<T> SelectOne(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes = null);
        Task<List<T>> SelectMany(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes = null, Dictionary<Expression<Func<T, object>>, bool> orderByAsc = null, int? page = null, int? pageSize = null);
        Task Insert(T entity);
        Task Insert(List<T> entities);
        Task Update(T entity);
        Task Update(List<T> entities);
        Task Delete(T entity);
        Task Delete(List<T> entities);
        Task Delete(Expression<Func<T, bool>> predicate);
        Task<bool> CheckExists(Expression<Func<T, bool>> predicate);
        Task<bool> CheckExists(string id);
        IQueryable<T> Queryable();
    }
}
