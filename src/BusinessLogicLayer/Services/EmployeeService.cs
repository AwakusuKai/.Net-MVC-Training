using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using BusinessLogicLayer.Mappers;

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
            EmployeeRepository.Create(Mapper.Convert<EmployeeDTO,Employee>(employeeDIO));
        }
        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();
            foreach (Employee employee in EmployeeRepository.GetAll())
            {
                employeeDTOs.Add(Mapper.Convert<Employee,EmployeeDTO>(employee));
            }
            return employeeDTOs;
        }

        public void UpdateEmployee(EmployeeDTO employeeDTO)
        {
            EmployeeRepository.Update(Mapper.Convert<EmployeeDTO,Employee>(employeeDTO));
        }
        public EmployeeDTO GetEmployee(int? id)
        {
            var employee = EmployeeRepository.GetById(id.Value);
            if (employee != null)
            {
                return Mapper.Convert<Employee, EmployeeDTO>(employee);
            }
            return null;
        }

        public void DeleteEmployee(int id)
        {
            EmployeeRepository.Delete(id);
        }
    }
}
