using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;

using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        IProjectService projectService;
        public HomeController(IProjectService serv)
        {
            projectService = serv;
        }

        public IActionResult Index()
        {
            IEnumerable<ProjectDTO> projectDtos = projectService.GetProjects();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, Project>()).CreateMapper();
            var projects = mapper.Map<IEnumerable<ProjectDTO>, List<Project>>(projectDtos);
            return View(projects);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Project projectModel)
        {
            if (ModelState.IsValid)
            {
                ProjectDTO projectDTO = new ProjectDTO { Name = projectModel.Name, ShortName = projectModel.ShortName, Description = projectModel.Description };
                projectService.CreateProject(projectDTO);
                return RedirectToAction("Index");
            }
            return View(projectModel);
        }

        public IActionResult Details(int id)
        {
            ProjectDTO projectDTO = projectService.GetProject(id);
            Project project = new Project { Id = projectDTO.Id, Name = projectDTO.Name, ShortName = projectDTO.ShortName, Description = projectDTO.Description };
            return View(project);
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
            Project project = new Project { Id = projectDTO.Id, Name = projectDTO.Name, ShortName = projectDTO.ShortName, Description = projectDTO.Description };
            return View(project);
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
                    Project project = new Project { Id = projectDTO.Id, Name = projectDTO.Name, ShortName = projectDTO.ShortName, Description = projectDTO.Description };
                    return View(project);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                ProjectDTO projectDTO = new ProjectDTO { Id = project.Id, Name = project.ShortName, ShortName = project.ShortName, Description = project.Description };
                projectService.UpdateProject(projectDTO);
                return RedirectToAction("Index");
            }
            return View(project);
            
        }
    }
}
