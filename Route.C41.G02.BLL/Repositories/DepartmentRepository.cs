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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(Department Entity)
        {
            _dbContext.Departments.Add(Entity);
            return _dbContext.SaveChanges();
        }
        public int Update(Department Entity)
        {
            _dbContext.Departments.Update(Entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(Department Entity)
        {
            _dbContext.Departments.Remove(Entity);
            return _dbContext.SaveChanges();
        }

        public Department Get(int id)
        {
            return _dbContext.Find<Department>(id);
           /// return _dbContext.Departments.Find(id);
           ///var Department = _dbContext.Departments.Local.Where(D => D.Id == id).FirstOrDefault();
           ///
           ///if(Department == null)
           ///{
           ///     Department = _dbContext.Departments.Where(D => D.Id == id).FirstOrDefault();
           ///}
           ///return Department;
        }

        public IEnumerable<Department> GetAll()
        {
            return _dbContext.Departments.AsNoTracking().ToList();
        }

    }
}
