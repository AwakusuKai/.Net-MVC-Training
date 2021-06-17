using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    public class EmployeeController : Controller
    {
        /*private readonly DataContext db;

        public EmployeeController(DataContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Employees.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = await db.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            return View(employeeModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,MiddleName,Position")] Employee employeeModel)
        {
            if (ModelState.IsValid)
            {
                db.Add(employeeModel);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeModel);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = await db.Employees.FindAsync(id);
            if (employeeModel == null)
            {
                return NotFound();
            }
            return View(employeeModel);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,MiddleName,Position")] Employee employeeModel)
        {
            if (id != employeeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(employeeModel);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeModelExists(employeeModel.Id))
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
            return View(employeeModel);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = await db.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            return View(employeeModel);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeModel = await db.Employees.FindAsync(id);
            db.Employees.Remove(employeeModel);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeModelExists(int id)
        {
            return db.Employees.Any(e => e.Id == id);
        }*/
    }
}
