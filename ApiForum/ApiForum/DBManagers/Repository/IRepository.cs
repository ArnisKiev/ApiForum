using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForum.Managers.Repository
{
    interface IRepository<T> where T:class
    {
        IEnumerable<T> GetItems();
        T Add(T item);
        void Delete(T item);
        void DeleteById(Guid id);
        void Update(T item);
        void Save();
         Task<T> FindById(Guid id);

    }
}
