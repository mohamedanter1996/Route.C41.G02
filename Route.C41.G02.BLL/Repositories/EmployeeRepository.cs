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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }
        public IQueryable<Employee> GetEmployeesByAddress(string Address)
        {
            return _dbContext.Employees.Where(E=>E.Address.ToLower() == Address.ToLower());
        }


        public IQueryable<Employee> SearchByName(string name) 
        {
            return _dbContext.Employees.Where(e => e.Name.ToLower().Contains(name));
        }
         
    }
}
