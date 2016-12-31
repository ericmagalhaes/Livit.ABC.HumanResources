using System;
using System.Linq;
using Livit.ABC.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Livit.ABC.Domain.Persistence
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly Repository _repository = null;
        public EmployeeRepository(Repository db)
        {
            _repository = db;
        }
        public Employee ApprovalManagerFromEmployee(string employeeId)
        {
            var employee = _repository.Employees.Include(e => e.Manager).FirstOrDefault(i => i.Id == employeeId);
            return employee;
        }
        /// <summary>
        /// Register a new employee
        /// </summary>
        /// <param name="email">new employee email must be unique</param>
        /// <param name="managerId">existing employee id usually employee email</param>
        /// <returns></returns>
        public Employee RegisterEmployee(string email, string managerId = null)
        {
            Employee employee = null;
            employee = _repository.Employees.Include(e=>e.Manager).FirstOrDefault(e => e.Id == email);
            if(employee != null)
                return employee;
            employee = new Employee
            {
                Id = email,
                Name = email
            };
            if (managerId != null)
            {
                var manager = _repository.Employees.FirstOrDefault(e => e.Id == managerId);
                employee.Manager = manager;
            }
            _repository.Employees.Add(employee);
            _repository.SaveChanges();
            return employee;
        }
    }
}