using BusinessLogicLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IEmployeeService
    {
        void CreateEmployee(EmployeeDTO employeeDTO);
        void UpdateEmployee(EmployeeDTO employeeDTO);
        void DeleteEmployee(int id);
        IEnumerable<EmployeeDTO> GetEmployees();
        EmployeeDTO GetEmployee(int? id);
    }
}
