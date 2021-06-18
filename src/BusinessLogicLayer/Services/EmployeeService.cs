using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        IRepository<Employee> EmployeeRepository { get; set; }
        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            EmployeeRepository = employeeRepository;
        }

        public void CreateEmployee(EmployeeDTO employeeDIO)
        {
            Employee employee = new Employee
            {
                Name = employeeDIO.Name,
                Surname = employeeDIO.Surname,
                MiddleName = employeeDIO.MiddleName,
                Position = employeeDIO.Position
            };
            EmployeeRepository.Create(employee);
        }
        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();
            foreach (Employee employee in EmployeeRepository.GetAll())
            {
                employeeDTOs.Add(new EmployeeDTO { Id = employee.Id, Name = employee.Name, Surname = employee.Surname, MiddleName = employee.MiddleName, Position=employee.Position });
            }

            return employeeDTOs;
        }

        public void UpdateEmployee(EmployeeDTO employeeDTO)
        {
            Employee employee = new Employee
            {
                Id = employeeDTO.Id,
                Name = employeeDTO.Name,
                Surname = employeeDTO.Surname,
                MiddleName = employeeDTO.MiddleName,
                Position = employeeDTO.Position
            };
            EmployeeRepository.Update(employee);
        }
        public EmployeeDTO GetEmployee(int? id)
        {
            var employee = EmployeeRepository.GetById(id.Value);
            if (employee != null)
            {
                return new EmployeeDTO { Name = employee.Name, Id = employee.Id, Surname = employee.Surname, MiddleName = employee.MiddleName, Position = employee.Position };
            }
            return null;
        }

        public void DeleteEmployee(int id)
        {
            EmployeeRepository.Delete(id);
        }
    }
}
