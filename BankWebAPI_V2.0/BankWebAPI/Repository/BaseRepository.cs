using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> dbSet;

        public BaseRepository(DbSet<T> dbSet)
        {
            this.dbSet = dbSet;
        }

        public virtual IQueryable<T> AsQueryable()
        {
            return dbSet.AsQueryable();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet;
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);

        }

        public void Create(T obj)
        {
            dbSet.Add(obj);
            
        }

        
        public void Update(T obj)
        {
            dbSet.AddOrUpdate(obj);
           
        }

        public bool Delete(int id)
        {
            var itemToRemove = dbSet.Find(id); //returns a single item.

            if (itemToRemove != null)
            {
                dbSet.Remove(itemToRemove);
               
                return true;
            }
            return false;
        }
 
    }
}
