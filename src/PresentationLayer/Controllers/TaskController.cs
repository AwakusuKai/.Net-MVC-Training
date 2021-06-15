using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Models;
using Task = PresentationLayer.Models.Task;

namespace PresentationLayer.Controllers
{
    public class TaskController : Controller
    {
        private readonly DataContext db;

        public TaskController(DataContext context)
        {
            db = context;
        }

        // GET: Task
        public async Task<IActionResult> Index()
        {
            var dataContext = db.Tasks.Include(t => t.Employee).Include(t => t.Project).Include(t => t.Status);
            return View(await dataContext.ToListAsync());
        }

        // GET: Task/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await db.Tasks
                .Include(t => t.Employee)
                .Include(t => t.Project)
                .Include(t => t.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Task/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(db.Employees, "Id", "FullNameAndPosition");
            ViewData["ProjectId"] = new SelectList(db.Projects, "Id", "Name");
            ViewData["StatusId"] = new SelectList(db.Statuses, "Id", "Name");
            return View();
        }

        // POST: Task/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ProjectId,EmployeeId,WorkTime,StartDate,CompletionDate,StatusId")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Add(task);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(db.Employees, "Id", "FullNameAndPosition", task.EmployeeId);
            ViewData["ProjectId"] = new SelectList(db.Projects, "Id", "Name", task.ProjectId);
            ViewData["StatusId"] = new SelectList(db.Statuses, "Id", "Name", task.StatusId);
            return View(task);
        }

        // GET: Task/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await db.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(db.Employees, "Id", "FullNameAndPosition", task.EmployeeId);
            ViewData["ProjectId"] = new SelectList(db.Projects, "Id", "Name", task.ProjectId);
            ViewData["StatusId"] = new SelectList(db.Statuses, "Id", "Name", task.StatusId);
            return View(task);
        }

        // POST: Task/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ProjectId,EmployeeId,WorkTime,StartDate,CompletionDate,StatusId")] Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(task);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(db.Employees, "Id", "FullNameAndPosition", task.EmployeeId);
            ViewData["ProjectId"] = new SelectList(db.Projects, "Id", "Name", task.ProjectId);
            ViewData["StatusId"] = new SelectList(db.Statuses, "Id", "Name", task.StatusId);
            return View(task);
        }

        // GET: Task/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await db.Tasks
                .Include(t => t.Employee)
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await db.Tasks.FindAsync(id);
            db.Tasks.Remove(task);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return db.Tasks.Any(e => e.Id == id);
        }
    }
}
