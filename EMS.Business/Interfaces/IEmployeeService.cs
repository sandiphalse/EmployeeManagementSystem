using EMS.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);

        Task<IEnumerable<Address>> GetAddressesByEmployeeIdAsync(int employeeId);

        Task<bool> UpdateAddressAsync(Address updatedAddress);

        Task<IEnumerable<Employee>> GetEmployeesByManagerIdAsync(int managerId);
    }
}
