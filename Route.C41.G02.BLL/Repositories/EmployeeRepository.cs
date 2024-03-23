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
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
           _dbContext = dbContext;
        }
        public int Add(Employee Entity)
        {
            _dbContext.Employees.Add(Entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(Employee Entity)
        {
            _dbContext.Employees.Remove(Entity);
            return _dbContext.SaveChanges();
        }

        public Employee Get(int id)
        {
            return _dbContext.Find<Employee>(id);
            /// return _dbContext.Employees.Find(id);
            ///var Employee = _dbContext.Employees.Local.Where(E => E.Id == id).FirstOrDefault();
            ///
            ///if(Employee == null)
            ///{
            ///     Employee = _dbContext.Employees.Where(E => E.Id == id).FirstOrDefault();
            ///}
            ///return Employees;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbContext.Employees.AsNoTracking().ToList();
        }

        public int Update(Employee Entity)
        {
            _dbContext.Employees.Update(Entity);
            return _dbContext.SaveChanges();
        }
    }
}
