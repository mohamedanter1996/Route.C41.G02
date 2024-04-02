using Route.C41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : ModelBase
    {
       Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        void Add(T Entity);

        void Update(T Entity);

        void Delete(T Entity);
    }
}
