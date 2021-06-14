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
        private DataContext db;
        public HomeController(DataContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Projects.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectModel = await db.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectModel == null)
            {
                return NotFound();
            }

            return View(projectModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Project projectModel)
        {
            db.Projects.Add(projectModel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Project project = await db.Projects.FirstOrDefaultAsync(p => p.Id == id);
                if (project != null)
                    return View(project);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Project project = await db.Projects.FirstOrDefaultAsync(p => p.Id == id);
                if (project != null)
                {
                    db.Projects.Remove(project);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Project project = await db.Projects.FirstOrDefaultAsync(p => p.Id == id);
                if (project != null)
                    return View(project);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Project user)
        {
            db.Projects.Update(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
