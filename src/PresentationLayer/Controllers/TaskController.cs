using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mappers;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentationLayer.Models;
using Task = PresentationLayer.Models.Task;

namespace PresentationLayer.Controllers
{
    public class TaskController : Controller
    {
        ITaskService taskService;
        IEmployeeService employeeService;
        IProjectService projectService;
        IStatusService statusService;
        public TaskController(ITaskService taskService, IEmployeeService employeeService, IProjectService projectService, IStatusService statusService)
        {
            this.taskService = taskService;
            this.employeeService = employeeService;
            this.projectService = projectService;
            this.statusService = statusService;
        }

        public IActionResult Index()
        {
            IEnumerable<TaskDTO> taskDtos = taskService.GetTasks();
            List<Task> tasks = new List<Task>();
            foreach (TaskDTO taskDTO in taskDtos)
            {
                Task task = Mapper.Convert<TaskDTO, Task>(taskDTO);
                task.Employee = Mapper.Convert<EmployeeDTO, Employee>(taskDTO.Employee);
                task.Project = Mapper.Convert<ProjectDTO, Project>(taskDTO.Project);
                task.Status = Mapper.Convert<StatusDTO, Status>(taskDTO.Status);
                tasks.Add(task);
            }
            return View(tasks);
        }

        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(Mapper.ConvertEnumerable<EmployeeDTO,Employee>(employeeService.GetEmployees()), "Id", "FullNameAndPosition");
            ViewData["ProjectId"] = new SelectList(Mapper.ConvertEnumerable<ProjectDTO, Project>(projectService.GetProjects()), "Id", "Name");
            ViewData["StatusId"] = new SelectList(Mapper.ConvertEnumerable<StatusDTO, Status>(statusService.GetStatuses()), "Id", "Name"); 
            return View();
        }

        [HttpPost]
        public IActionResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                TaskDTO taskDTO = Mapper.Convert<Task, TaskDTO>(task);
                /*taskDTO.Project = Mapper.Convert<Project, ProjectDTO>(task.Project);
                taskDTO.Employee = Mapper.Convert<Employee, EmployeeDTO>(task.Employee);
                taskDTO.Status = Mapper.Convert<Status, StatusDTO>(task.Status);*/
                taskService.CreateTask(taskDTO);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        public IActionResult Details(int id)
        {
            TaskDTO taskDTO = taskService.GetTask(id);
            if (taskDTO != null)
            {
                Task task = Mapper.Convert<TaskDTO, Task>(taskDTO);
                task.Employee = Mapper.Convert<EmployeeDTO, Employee>(taskDTO.Employee);
                task.Project = Mapper.Convert<ProjectDTO, Project>(taskDTO.Project);
                task.Status = Mapper.Convert<StatusDTO, Status>(taskDTO.Status);
                return View(task);
            }
            return NotFound();

        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            TaskDTO taskDTO = taskService.GetTask(id);
            if (taskDTO == null)
            {
                return NotFound();
            }
            Task task = Mapper.Convert<TaskDTO, Task>(taskDTO);
            task.Employee = Mapper.Convert<EmployeeDTO, Employee>(taskDTO.Employee);
            task.Project = Mapper.Convert<ProjectDTO, Project>(taskDTO.Project);
            task.Status = Mapper.Convert<StatusDTO, Status>(taskDTO.Status);
            return View(task);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            taskService.DeleteTask(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            ViewData["EmployeeId"] = new SelectList(Mapper.ConvertEnumerable<EmployeeDTO, Employee>(employeeService.GetEmployees()), "Id", "FullNameAndPosition");
            ViewData["ProjectId"] = new SelectList(Mapper.ConvertEnumerable<ProjectDTO, Project>(projectService.GetProjects()), "Id", "Name");
            ViewData["StatusId"] = new SelectList(Mapper.ConvertEnumerable<StatusDTO, Status>(statusService.GetStatuses()), "Id", "Name");
            if (id != null)
            {
                TaskDTO taskDTO = taskService.GetTask(id);
                if (taskDTO != null)
                {
                    Task task = Mapper.Convert<TaskDTO, Task>(taskDTO);
                    task.Employee = Mapper.Convert<EmployeeDTO, Employee>(taskDTO.Employee);
                    task.Project = Mapper.Convert<ProjectDTO, Project>(taskDTO.Project);
                    task.Status = Mapper.Convert<StatusDTO, Status>(taskDTO.Status);
                    return View(task);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                taskService.UpdateTask(Mapper.Convert<Task,TaskDTO>(task));
                return RedirectToAction("Index");
            }
            return View(task);

        }
    }
}
