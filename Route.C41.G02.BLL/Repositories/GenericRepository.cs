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
        public int Add(T Entity)
        {
            _dbContext.Set<T>().Add(Entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(T Entity)
        {
            _dbContext.Set<T>().Remove(Entity);
            return _dbContext.SaveChanges();
        }
        public int Update(T Entity)
        {
            _dbContext.Set<T>().Update(Entity);
            return _dbContext.SaveChanges();
        }

        public T Get(int id)
        {
            return _dbContext.Find<T>(id);
            /// return _dbContext.Employees.Find(id);
            ///var GenericType = _dbContext.Set<T>().Local.Where(T => T.Id == id).FirstOrDefault();
            ///
            ///if(GenericType == null)
            ///{
            ///     GenericType = _dbContext.Set<T>().Where(T => T.Id == id).FirstOrDefault();
            ///}
            ///return GenericType;

        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T)==typeof(Employee)) 
            {
                return (IEnumerable<T>)_dbContext.Set<Employee>().Include(E => E.Department).AsNoTracking().ToList();
            }
            else { return _dbContext.Set<T>().AsNoTracking().ToList(); }
           
        }

    }
}
