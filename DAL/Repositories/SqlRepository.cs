using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using System.Data.Entity;

namespace DAL.Repositories
{
    public class SqlRepository : ISqlRepository
    {
        private readonly IAppDBContext _context;
        public SqlRepository(IAppDBContext context)
        {
            _context = context;
        }

        private IDbSet<TEntity> GetEntities<TEntity>() where TEntity: class
        {
            return _context.Set<TEntity>();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            GetEntities<TEntity>().Remove(entity);
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            GetEntities<TEntity>().Add(entity);
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return GetEntities<TEntity>();
        }

        public TEntity GetById<TEntity>(int id) where TEntity : BaseModel<int>
        {
            return GetAll<TEntity>().SingleOrDefault(x => x.Id == id);
        }

        public TEntity GetById<TEntity>(string id) where TEntity : BaseModel<string>
        {
            return GetAll<TEntity>().SingleOrDefault(x => x.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if( _context != null )
            {
                _context.Dispose();
            }
        }
    }
}
