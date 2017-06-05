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
    public class PlayEventsController : Controller
    {
        private readonly ChildrensActivityLogContext _context;

        public PlayEventsController(ChildrensActivityLogContext context)
        {
            _context = context;    
        }

        // GET: PlayEvents
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlayEvents.ToListAsync());
        }

        // GET: PlayEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playEvent = await _context.PlayEvents
                .SingleOrDefaultAsync(m => m.Id == id);
            if (playEvent == null)
            {
                return NotFound();
            }

            return View(playEvent);
        }

        // GET: PlayEvents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlayEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,StartDate")] PlayEvent playEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(playEvent);
        }

        // GET: PlayEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playEvent = await _context.PlayEvents.SingleOrDefaultAsync(m => m.Id == id);
            if (playEvent == null)
            {
                return NotFound();
            }
            return View(playEvent);
        }

        // POST: PlayEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,StartDate")] PlayEvent playEvent)
        {
            if (id != playEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayEventExists(playEvent.Id))
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
            return View(playEvent);
        }

        // GET: PlayEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playEvent = await _context.PlayEvents
                .SingleOrDefaultAsync(m => m.Id == id);
            if (playEvent == null)
            {
                return NotFound();
            }

            return View(playEvent);
        }

        // POST: PlayEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playEvent = await _context.PlayEvents.SingleOrDefaultAsync(m => m.Id == id);
            _context.PlayEvents.Remove(playEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PlayEventExists(int id)
        {
            return _context.PlayEvents.Any(e => e.Id == id);
        }
    }
}
