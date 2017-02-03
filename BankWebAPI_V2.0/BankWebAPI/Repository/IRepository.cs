using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> AsQueryable();

        T GetById(int id);
        IEnumerable<T> GetAll();
        void Create(T obj);
        void Update(T obj);
        bool Delete(int id);
    }
}
