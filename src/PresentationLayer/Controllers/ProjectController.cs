using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task = PresentationLayer.Models.Task;

namespace PresentationLayer.Controllers
{
    public class ProjectController : Controller
    {
        IProjectService projectService;
        ILogger logger;

        public ProjectController(IProjectService projectService, ILogger<ProjectController> logger)
        {
            this.projectService = projectService;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            logger.LogInformation("Project/Index Get request");
            return View(Mapper.ConvertEnumerable<ProjectDTO,Project>(projectService.GetProjects()));
        }

        public IActionResult Create()
        {
            logger.LogInformation("Project/Create Get request");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Project project)
        {
            logger.LogInformation("Project/Create Post request");
            if (ModelState.IsValid)
            {
                projectService.CreateProject(Mapper.Convert<Project, ProjectDTO>(project));
                return RedirectToAction("Index");
            }
            return View(project);
        }

        public IActionResult Details(int id)
        {
            logger.LogInformation("Project/Details Get request");
            ProjectDTO projectDTO = projectService.GetProject(id);
            if(projectDTO != null)
            {
                return View(Mapper.Convert<ProjectDTO, Project>(projectDTO));
            }
            return NotFound();
            
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            logger.LogInformation($"Project/Delete/{id} Get request");
            ProjectDTO projectDTO = projectService.GetProject(id);
            if (projectDTO == null)
            {
                return NotFound();
            }
            return View(Mapper.Convert<ProjectDTO, Project>(projectDTO));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            logger.LogInformation($"Project/Delete/{id} Post request");
            projectService.DeleteProject(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            logger.LogInformation($"Project/Edit/{id} Get request");
            if (id != null)
            {
                ProjectDTO projectDTO = projectService.GetProject(id);
                if (projectDTO != null)
                {
                    return View(Mapper.Convert<ProjectDTO, Project>(projectDTO));
                }
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(Project project)
        {
            logger.LogInformation("Project/Edit Post request");
            if (ModelState.IsValid)
            {
                projectService.UpdateProject(Mapper.Convert<Project,ProjectDTO>(project));
                return RedirectToAction("Index");
            }
            return View(project);
            
        }
    }
}
