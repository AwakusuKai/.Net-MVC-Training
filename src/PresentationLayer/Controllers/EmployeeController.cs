using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentationLayer.Models;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.DTO;

namespace PresentationLayer.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeService employeeService;
        public EmployeeController(IEmployeeService serv)
        {
            employeeService = serv;
        }

        public IActionResult Index()
        {
            IEnumerable<EmployeeDTO> employeesDtos = employeeService.GetEmployees();
            List<Employee> employees = new List<Employee>();
            foreach (EmployeeDTO employeeDTO in employeesDtos)
            {
                employees.Add(new Employee { Id = employeeDTO.Id, Name = employeeDTO.Name, Surname = employeeDTO.Surname, MiddleName = employeeDTO.MiddleName , Position = employeeDTO.Position});
            }
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                EmployeeDTO employeeDTO = new EmployeeDTO { Name = employee.Name, Surname = employee.Surname, MiddleName = employee.MiddleName, Position = employee.Position };
                employeeService.CreateEmployee(employeeDTO);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public IActionResult Details(int id)
        {
            EmployeeDTO employeeDTO = employeeService.GetEmployee(id);
            if (employeeDTO != null)
            {
                Employee employee = new Employee { Id = employeeDTO.Id, Name = employeeDTO.Name, Surname = employeeDTO.Surname, MiddleName = employeeDTO.MiddleName, Position = employeeDTO.Position };
                return View(employee);
            }
            return NotFound();

        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            EmployeeDTO employeeDTO = employeeService.GetEmployee(id);
            if (employeeDTO == null)
            {
                return NotFound();
            }
            Employee employee = new Employee { Id = employeeDTO.Id, Name = employeeDTO.Name, Surname = employeeDTO.Surname, MiddleName = employeeDTO.MiddleName, Position = employeeDTO.Position };
            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            employeeService.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                EmployeeDTO employeeDTO = employeeService.GetEmployee(id);
                if (employeeDTO != null)
                {
                    Employee employee = new Employee { Id = employeeDTO.Id, Name = employeeDTO.Name, Surname = employeeDTO.Surname, MiddleName = employeeDTO.MiddleName, Position = employeeDTO.Position };
                    return View(employee);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                EmployeeDTO employeeDTO = new EmployeeDTO { Id = employee.Id, Name = employee.Name, Surname = employee.Surname, MiddleName = employee.MiddleName, Position = employee.Position };
                employeeService.UpdateEmployee(employeeDTO);
                return RedirectToAction("Index");
            }
            return View(employee);

        }
    }
}
