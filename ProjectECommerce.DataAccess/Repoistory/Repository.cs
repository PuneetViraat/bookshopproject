using Microsoft.EntityFrameworkCore;
using ProjectECommerce.DataAccess.Data;
using ProjectECommerce.DataAccess.Repoistory.IRepoistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectECommerce.DataAccess.Repoistory
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbset;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            dbset=context.Set<T>();
        }

        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> filter = null, string includeproperties = null)
        {
            IQueryable<T> query = dbset;
            if(filter!=null)
                query = query.Where(filter);
            if(includeproperties!=null)
            {
                foreach (var includeProp in includeproperties.Split(new[] {','},
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);

                }
            }
            return query.FirstOrDefault();
        }

        public T Get(int id)
        {
            return dbset.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeproperties = null) // Category,Cover Type,Product
        {
            IQueryable<T> query = dbset;
            if(filter!=null)
                query = query.Where(filter);
            if(includeproperties!=null)
            {
                foreach (var includeprop in includeproperties.Split(new[] {','},
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);

                }
            }
            if (orderBy != null)
                return orderBy(query).ToList();
            return query.ToList();
            
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void Remove(int id)
        {
         //   T entity = Get(id);
            T entity = dbset.Find(id);  
            dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> values)
        {
            dbset.RemoveRange(values);
        }

        public void Update(T entity)
        {
            _context.ChangeTracker.Clear();
            dbset.Update(entity);
        }
    }
}
