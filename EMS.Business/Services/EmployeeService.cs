using EMS.Business.Interfaces;
using EMS.Repository.Interfaces;
using EMS.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            return await _repository.AddEmployeeAsync(employee);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _repository.GetAllEmployeesAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _repository.GetEmployeeByIdAsync(id);
        }

        public async Task<IEnumerable<Address>> GetAddressesByEmployeeIdAsync(int employeeId)
        {
            return await _repository.GetAddressesByEmployeeIdAsync(employeeId);
        }

        public async Task<bool> UpdateAddressAsync(Address updatedAddress)
        {
            return await _repository.UpdateAddressAsync(updatedAddress);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByManagerIdAsync(int managerId)
        {
            return await _repository.GetEmployeesByManagerIdAsync(managerId);
        }

    }
}
