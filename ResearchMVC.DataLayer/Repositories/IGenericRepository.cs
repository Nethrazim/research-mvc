using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ResearchMVC.DataLayer.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Delete(TEntity entity);
        void Delete(object id);
        Task<List<TEntity>> Get<P>(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, P>> orderBy = null, string includeProperties = "");
        Task<(IEnumerable<TEntity> entities, int total)> GetPaged<P>(int skip, int take, Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity,P>> orderBy = null, string includeProperties = "");
        Task<TEntity> GetById(object id);
        IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void SetEntityStateModified(TEntity entity);
        TEntity Find(object id);
        bool IsDetached(TEntity entity);
    }
}
