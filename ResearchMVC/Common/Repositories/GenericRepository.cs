﻿using ResearchMVC.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace ResearchMVC.Common.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public AdventureWorksModel dbContext;
        public DbSet<TEntity> dbSet;

        public GenericRepository(AdventureWorksModel context)
        {
            this.dbContext = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual void Delete(TEntity entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }

        public void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public async Task<List<TEntity>> Get<P>(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity,P>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            dbContext.Database.Log = (s) => Debug.WriteLine(s);

            if (orderBy != null)
            {
                return await query.OrderBy(orderBy).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<(IEnumerable<TEntity> entities, int total)> GetPaged<P>(int skip, int take, Expression<Func<TEntity, bool>> filter = null,  Expression<Func<TEntity,P>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            dbContext.Database.Log = (s) => Debug.WriteLine(s);
            
            var entities = await query.Skip(skip).Take(take).ToListAsync();

            int total = await query.CountAsync();

            return (entities, total);
        }

        public async Task<TEntity> GetById(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return dbSet.SqlQuery(query, parameters).ToList();
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}