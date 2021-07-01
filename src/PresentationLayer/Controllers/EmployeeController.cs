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
using BusinessLogicLayer.Mappers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace PresentationLayer.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeService employeeService;
        ILogger logger;
        public EmployeeController(IEmployeeService serv, ILogger<EmployeeController> logger)
        {
            this.logger = logger;
            employeeService = serv;

        }

        public IActionResult Index()
        {
            logger.LogInformation("Employee/Index Get request");
            return View(Mapper.ConvertEnumerable<EmployeeDTO, Employee>(employeeService.GetEmployees()));
        }

        public IActionResult Create()
        {
            logger.LogInformation("Employee/Create Get request");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            logger.LogInformation("Employee/Create Post request");

            if (ModelState.IsValid)
            {
                employeeService.CreateEmployee(Mapper.Convert<Employee,EmployeeDTO>(employee));
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public IActionResult Details(int id)
        {
            logger.LogInformation("Employee/Details Get request");
            EmployeeDTO employeeDTO = employeeService.GetEmployee(id);
            if (employeeDTO != null)
            { 
                return View(Mapper.Convert<EmployeeDTO, Employee>(employeeDTO));
            }
            return NotFound();

        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            logger.LogInformation($"Employee/Delete/{id} Get request");
            EmployeeDTO employeeDTO = employeeService.GetEmployee(id);
            if (employeeDTO == null)
            {
                return NotFound();
            }
            return View(Mapper.Convert<EmployeeDTO,Employee>(employeeDTO));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            logger.LogInformation($"Employee/Delete/{id} Post request");
            employeeService.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            logger.LogInformation($"Employee/Edit/{id} Get request");
            if (id != null)
            {
                EmployeeDTO employeeDTO = employeeService.GetEmployee(id);
                if (employeeDTO != null)
                {
                    return View(Mapper.Convert<EmployeeDTO, Employee>(employeeDTO));
                }
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            logger.LogInformation("Employee/Edit Post request");
            if (ModelState.IsValid)
            {
                employeeService.UpdateEmployee(Mapper.Convert<Employee,EmployeeDTO>(employee));
                return RedirectToAction("Index");
            }
            return View(employee);

        }
    }
}
