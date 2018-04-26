using NewApp.Database;
using NewApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NewApp.Repository.Generic
{
    public class Repository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly AppDbContext context;
        private DbSet<T> entities;
        public Repository(AppDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public async Task<List<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> Get(string id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<T> SelectOne(Expression<Func<T, bool>> predicate,
            List<Expression<Func<T, object>>> includes = null)
        {
            var query = Query(predicate: predicate, includes: includes, page: 1, pageSize: 1);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> SelectMany(Expression<Func<T, bool>> predicate,
            List<Expression<Func<T, object>>> includes = null,
            Dictionary<Expression<Func<T, object>>, bool> orderByAsc = null,
            int? page = null,
            int? pageSize = null)
        {
            var query = Query(predicate: predicate, includes: includes, orderByAsc: orderByAsc, page: page, pageSize: pageSize);
            return await query.ToListAsync();
        }

        public async Task Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Insert(List<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException("entity");
            }
            await this.entities.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            this.entities.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(List<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException("entity");
            }
            this.entities.UpdateRange(entities);
            await context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(List<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException("entity");
            }
            this.entities.RemoveRange(entities);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Expression<Func<T, bool>> predicate)
        {
            var entitiesDel = await entities.Where(predicate).ToListAsync();
            if (entitiesDel.Any())
                await Delete(entitiesDel);
        }

        public async Task<bool> CheckExists(Expression<Func<T, bool>> predicate)
        {
            return await entities.AnyAsync(predicate);
        }

        public async Task<bool> CheckExists(string id)
        {
            return await entities.AnyAsync(x => x.Id == id);
        }

        public IQueryable<T> Queryable()
        {
            return entities;
        }

        private IQueryable<T> Query(Expression<Func<T, bool>> predicate = null,
            List<Expression<Func<T, object>>> includes = null,
            Dictionary<Expression<Func<T, object>>, bool> orderByAsc = null,
            int? page = null,
            int? pageSize = null)
        {
            var query = entities.AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            if (includes != null && includes.Any())
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (orderByAsc != null && orderByAsc.Any())
            {
                query = orderByAsc.First().Value
                    ? query.OrderBy(orderByAsc.First().Key)
                    : query.OrderByDescending(orderByAsc.First().Key);
                orderByAsc.Remove(orderByAsc.First().Key);
                query = orderByAsc.Aggregate(query, (current, order) => order.Value
                    ? (current as IOrderedQueryable<T>).ThenBy(order.Key)
                    : (current as IOrderedQueryable<T>).ThenByDescending(order.Key));
            }

            if (page != null)
                query = query.Skip((page.Value - 1) * pageSize.Value);

            if (pageSize != null)
                query = query.Take(pageSize.Value);

            return query;
        }
    }
}
