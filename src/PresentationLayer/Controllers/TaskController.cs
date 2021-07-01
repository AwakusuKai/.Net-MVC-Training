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
using Microsoft.Extensions.Logging;
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
        ILogger logger;
        public TaskController(ITaskService taskService, IEmployeeService employeeService, IProjectService projectService, IStatusService statusService, ILogger<TaskController> logger)
        {
            this.taskService = taskService;
            this.employeeService = employeeService;
            this.projectService = projectService;
            this.statusService = statusService;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            logger.LogInformation("Task/Index Get request");
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
            logger.LogInformation("Task/Create Get request");
            ViewData["EmployeeId"] = new SelectList(Mapper.ConvertEnumerable<EmployeeDTO,Employee>(employeeService.GetEmployees()), "Id", "FullNameAndPosition");
            ViewData["ProjectId"] = new SelectList(Mapper.ConvertEnumerable<ProjectDTO, Project>(projectService.GetProjects()), "Id", "Name");
            ViewData["StatusId"] = new SelectList(Mapper.ConvertEnumerable<StatusDTO, Status>(statusService.GetStatuses()), "Id", "Name"); 
            return View();
        }

        [HttpPost]
        public IActionResult Create(Task task)
        {
            logger.LogInformation("Task/Create Post request");
            if (ModelState.IsValid)
            {
                TaskDTO taskDTO = Mapper.Convert<Task, TaskDTO>(task);
                taskService.CreateTask(taskDTO);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        public IActionResult Details(int id)
        {
            logger.LogInformation("Task/Details Get request");
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
            logger.LogInformation($"Task/Delete/{id} Get request");
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
            logger.LogInformation($"Task/Delete/{id} Post request");
            taskService.DeleteTask(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            logger.LogInformation($"Task/Edit/{id} Get request");
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
            logger.LogInformation("Task/Edit Post request");
            if (ModelState.IsValid)
            {
                taskService.UpdateTask(Mapper.Convert<Task,TaskDTO>(task));
                return RedirectToAction("Index");
            }
            return View(task);

        }
    }
}
