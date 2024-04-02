using Microsoft.EntityFrameworkCore;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.DAL.Data;
using Route.C41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public  void Add(T Entity)
        {
            _dbContext.Set<T>().Add(Entity);
           
        }

        public  void Delete(T Entity)
        {
            _dbContext.Set<T>().Remove(Entity);
           
        }
        public void Update(T Entity)
        {
            _dbContext.Set<T>().Update(Entity);
            
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbContext.FindAsync<T>(id);
            /// return _dbContext.Employees.Find(id);
            ///var GenericType = _dbContext.Set<T>().Local.Where(T => T.Id == id).FirstOrDefault();
            ///
            ///if(GenericType == null)
            ///{
            ///     GenericType = _dbContext.Set<T>().Where(T => T.Id == id).FirstOrDefault();
            ///}
            ///return GenericType;

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T)==typeof(Employee)) 
            {
                return (IEnumerable<T>) await _dbContext.Set<Employee>().Include(E => E.Department).AsNoTracking().ToListAsync();
            }
            else { return await _dbContext.Set<T>().AsNoTracking().ToListAsync(); }
           
        }

    }
}
