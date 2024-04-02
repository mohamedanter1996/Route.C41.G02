using Microsoft.EntityFrameworkCore.Metadata;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.DAL.Data;
using Route.C41.G02.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        private Hashtable _ripositories;



        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _ripositories=new Hashtable();
        }

        public IGenericRepository<T> Repository<T>() where T : ModelBase
        {
            var Key = typeof(T).Name;

            if (!_ripositories.ContainsKey(Key))
            {
           
                if (Key==nameof(Employee))
                {
                    var repository = new EmployeeRepository(_dbContext);
                    _ripositories.Add(Key, repository);
                }
                else
                {
                    var repository = new GenericRepository<T>(_dbContext);
                    _ripositories.Add(Key, repository);
                }


            }
            

            return _ripositories[Key] as IGenericRepository<T>;
        }
      
        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync() 
        {
           await _dbContext.DisposeAsync();
        }

    }
}
