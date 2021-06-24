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

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public IActionResult Index()
        {
            return View(Mapper.ConvertEnumerable<ProjectDTO,Project>(projectService.GetProjects()));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                projectService.CreateProject(Mapper.Convert<Project, ProjectDTO>(project));
                return RedirectToAction("Index");
            }
            return View(project);
        }

        public IActionResult Details(int id)
        {
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
            projectService.DeleteProject(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
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
            if (ModelState.IsValid)
            {
                projectService.UpdateProject(Mapper.Convert<Project,ProjectDTO>(project));
                return RedirectToAction("Index");
            }
            return View(project);
            
        }
    }
}
