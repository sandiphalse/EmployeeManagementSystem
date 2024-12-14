namespace EMS.Tests
{
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;
    using EMS.Business.Services;
    using EMS.Repository.Interfaces;
    using EMS.Repository.Models;

    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepository;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_mockRepository.Object);
        }

        [Fact]
        public async Task CreateEmployeeAsync_ShouldReturnEmployee_WhenEmployeeIsCreated()
        {
            var employee = new Employee
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Designation = "Software Engineer",
                ManagerId = 2
            };

            _mockRepository.Setup(repo => repo.AddEmployeeAsync(It.IsAny<Employee>()))
                .ReturnsAsync(employee); 

            var result = await _employeeService.CreateEmployeeAsync(employee);

            Assert.NotNull(result);
            Assert.Equal(employee.Id, result.Id);
            Assert.Equal(employee.FirstName, result.FirstName);
            Assert.Equal(employee.LastName, result.LastName);
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ShouldReturnListOfEmployees()
        {
            var employees = new List<Employee>
        {
            new Employee { Id = 1, FirstName = "John", LastName = "Doe", Designation = "Software Engineer" },
            new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", Designation = "Product Manager" }
        };

            _mockRepository.Setup(repo => repo.GetAllEmployeesAsync())
                .ReturnsAsync(employees);

            var result = await _employeeService.GetAllEmployeesAsync();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ShouldReturnEmployee_WhenEmployeeExists()
        {
            var employee = new Employee { Id = 1, FirstName = "John", LastName = "Doe", Designation = "Software Engineer" };

            _mockRepository.Setup(repo => repo.GetEmployeeByIdAsync(1))
                .ReturnsAsync(employee); 

            var result = await _employeeService.GetEmployeeByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(employee.Id, result.Id);
            Assert.Equal(employee.FirstName, result.FirstName);
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ShouldReturnNull_WhenEmployeeDoesNotExist()
        {
            _mockRepository.Setup(repo => repo.GetEmployeeByIdAsync(99))
                .ReturnsAsync((Employee)null); 

            var result = await _employeeService.GetEmployeeByIdAsync(99);

            Assert.Null(result);
        }
    }

}