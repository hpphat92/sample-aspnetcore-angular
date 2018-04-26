using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using NewApp.Entities;
using NewApp.Models.DTOs;
using NewApp.Repository.Generic;
using AutoMapper;

namespace NewApp.Services.Generic
{
    public class Service<TEntity, TEntityDTO> : IService<TEntity, TEntityDTO> where TEntity : class, IBaseEntity where TEntityDTO : BaseModel
    {
        protected readonly IRepository<TEntity> repository;
        protected readonly IMapper mapper;
        public Service(IRepository<TEntity> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public virtual async Task<List<TEntityDTO>> GetAll()
        {
            var entities = await repository.GetAll();
            var result = mapper.Map<List<TEntityDTO>>(entities);
            return result;
        }

        public virtual async Task<TEntityDTO> Get(string id)
        {
            var entity = await repository.Get(id);
            var result = mapper.Map<TEntityDTO>(entity);
            return result;
        }

        public virtual async Task<TEntityDTO> SelectOne(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> includes = null)
        {
            var entity = await repository.SelectOne(predicate, includes);
            var resp = mapper.Map<TEntityDTO>(entity);
            return resp;
        }

        public virtual async Task<List<TEntityDTO>> SelectMany(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object>>> includes = null,
            Dictionary<Expression<Func<TEntity, object>>, bool> orderByAsc = null, int? page = null, int? pageSize = null)
        {
            var entities = await repository.SelectMany(predicate, includes, orderByAsc, page, pageSize);
            var resp = mapper.Map<List<TEntityDTO>>(entities);
            return resp;
        }

        public virtual async Task<TEntity> Insert(TEntityDTO model, bool isCheckExists = true)
        {
            TEntity entity = null;

            if (isCheckExists)
                entity = await repository.Get(model.Id);

            if (entity == null)
            {
                entity = mapper.Map<TEntity>(model);
                await repository.Insert(entity);
            }

            return entity;
        }

        public virtual async Task<TEntity> Update(TEntityDTO model)
        {
            var entity = await repository.Get(model.Id);
            var entityUpdate = mapper.Map(model, entity);
            await repository.Update(entityUpdate);
            return entityUpdate;
        }

        public virtual async Task<TEntity> UpSert(TEntityDTO model)
        {
            if (await repository.CheckExists(model.Id))
                return await Update(model);
            else
                return await Insert(model);
        }

        public virtual async Task Delete(string id)
        {
            await repository.Delete(x => x.Id == id);
        }

        public virtual async Task<bool> CheckExists(Expression<Func<TEntity, bool>> predicate)
        {
            return await repository.CheckExists(predicate);
        }

        public virtual async Task<bool> CheckExists(string id)
        {
            return await repository.CheckExists(id);
        }
    }
}
