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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProjectModel projectModel)
        {
            db.Projects.Add(projectModel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
