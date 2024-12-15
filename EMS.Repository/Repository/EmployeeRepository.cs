using EMS.Repository.Data;
using EMS.Repository.Interfaces;
using EMS.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Repository.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }



        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Address>> GetAddressesByEmployeeIdAsync(int employeeId)
        {
            return await _context.Addresses
                .Where(a => a.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<bool> UpdateAddressAsync(Address updatedAddress)
        {
            var existingAddress = await _context.Addresses.FindAsync(updatedAddress.Id);
            if (existingAddress == null)
            {
                return false; 
            }

            existingAddress.City = updatedAddress.City;
            existingAddress.Area = updatedAddress.Area;
            existingAddress.PinCode = updatedAddress.PinCode;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByManagerIdAsync(int managerId)
        {
            return await _context.Employees
                                 .Where(e => e.ManagerId == managerId)
                                 .ToListAsync();
        }
    }
}
