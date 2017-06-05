using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChildrensActivityLog2.Models;
using ChildrensActivityLog2.Repositories;

namespace ChildrensActivityLog2.Controllers
{
    public class SleepingPeriodsController : Controller
    {
        private readonly ChildrensActivityLogContext _context;

        public SleepingPeriodsController(ChildrensActivityLogContext context)
        {
            _context = context;    
        }

        // GET: SleepingPeriods
        public async Task<IActionResult> Index()
        {
            var childrensActivityLogContext = _context.SleepingPeriods.Include(s => s.Child);
            return View(await childrensActivityLogContext.ToListAsync());
        }

        // GET: SleepingPeriods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sleepingPeriod = await _context.SleepingPeriods
                .Include(s => s.Child)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sleepingPeriod == null)
            {
                return NotFound();
            }

            return View(sleepingPeriod);
        }

        // GET: SleepingPeriods/Create
        public IActionResult Create()
        {
            ViewData["ChildId"] = new SelectList(_context.Children, "Id", "Id");
            return View();
        }

        // POST: SleepingPeriods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ChildId,From,To,TypeOfSleepingPeriod")] SleepingPeriod sleepingPeriod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sleepingPeriod);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ChildId"] = new SelectList(_context.Children, "Id", "Id", sleepingPeriod.ChildId);
            return View(sleepingPeriod);
        }

        // GET: SleepingPeriods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sleepingPeriod = await _context.SleepingPeriods.SingleOrDefaultAsync(m => m.Id == id);
            if (sleepingPeriod == null)
            {
                return NotFound();
            }
            ViewData["ChildId"] = new SelectList(_context.Children, "Id", "Id", sleepingPeriod.ChildId);
            return View(sleepingPeriod);
        }

        // POST: SleepingPeriods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChildId,From,To,TypeOfSleepingPeriod")] SleepingPeriod sleepingPeriod)
        {
            if (id != sleepingPeriod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sleepingPeriod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SleepingPeriodExists(sleepingPeriod.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ChildId"] = new SelectList(_context.Children, "Id", "Id", sleepingPeriod.ChildId);
            return View(sleepingPeriod);
        }

        // GET: SleepingPeriods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sleepingPeriod = await _context.SleepingPeriods
                .Include(s => s.Child)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sleepingPeriod == null)
            {
                return NotFound();
            }

            return View(sleepingPeriod);
        }

        // POST: SleepingPeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sleepingPeriod = await _context.SleepingPeriods.SingleOrDefaultAsync(m => m.Id == id);
            _context.SleepingPeriods.Remove(sleepingPeriod);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SleepingPeriodExists(int id)
        {
            return _context.SleepingPeriods.Any(e => e.Id == id);
        }
    }
}
