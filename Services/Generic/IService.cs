using NewApp.Entities;
using NewApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NewApp.Services.Generic
{
    public interface IService<TEntity, TEntityDTO> where TEntity : IBaseEntity where TEntityDTO : BaseModel
    {
        Task<List<TEntityDTO>> GetAll();
        Task<TEntityDTO> Get(string id);
        Task<TEntityDTO> SelectOne(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> includes = null);
        Task<List<TEntityDTO>> SelectMany(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> includes = null, Dictionary<Expression<Func<TEntity, object>>, bool> orderByAsc = null, int? page = null, int? pageSize = null);
        Task<TEntity> Insert(TEntityDTO model, bool isCheckExists = true);
        Task<TEntity> Update(TEntityDTO model);
        Task<TEntity> UpSert(TEntityDTO model);
        Task Delete(string id);
        Task<bool> CheckExists(Expression<Func<TEntity, bool>> predicate);
        Task<bool> CheckExists(string id);
    }
}
