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
            return View(Mapper.ConvertEnumerable<EmployeeDTO, Employee>(employeeService.GetEmployees()));
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
                employeeService.CreateEmployee(Mapper.Convert<Employee,EmployeeDTO>(employee));
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public IActionResult Details(int id)
        {
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
                    return View(Mapper.Convert<EmployeeDTO, Employee>(employeeDTO));
                }
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employeeService.UpdateEmployee(Mapper.Convert<Employee,EmployeeDTO>(employee));
                return RedirectToAction("Index");
            }
            return View(employee);

        }
    }
}
