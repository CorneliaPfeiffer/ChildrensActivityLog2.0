using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChildrensActivityLog2.Models;
using ChildrensActivityLog2.Repositories;
using ChildrensActivityLog2.ViewModels;
using System.Collections.Generic;

namespace ChildrensActivityLog2.Controllers
{
    public class ChildrensPlayEventsController : Controller
    {
        private readonly ChildrensActivityLogContext _context;

        public ChildrensPlayEventsController(ChildrensActivityLogContext context)
        {
            _context = context;
        }

        // GET: ChildrensPlayEvents
        public async Task<IActionResult> Index()
        {
            var childrensActivityLogContext = _context.ChildrensPlayEvents.Include(c => c.Child).Include(c => c.PlayEvent);
            return View(await childrensActivityLogContext.ToListAsync());
        }

        private void PopulateChildrenDropDown(ChildrensPlayEventsViewModel viewModel)
        {
            var _Children = _context.Children;
            viewModel.Children = new List<SelectListItem>();
            foreach (var child in _Children)
            {
                viewModel.Children.Add(
                    new SelectListItem
                    {
                        Text = $"{child.FirstName} {child.LastName}",
                        Value = $"{child.Id.ToString()}"
                    });
            }
        }

        private void PopulatePlayEventDropDown(ChildrensPlayEventsViewModel viewModel)
        {
            var _PlayEvents = _context.PlayEvents;           
            viewModel.PlayEvents = new List<SelectListItem>();
            foreach (var pe in _PlayEvents)
            {
                if(pe != null)
                { 
                viewModel.PlayEvents.Add(
                    new SelectListItem
                    {
                        Text = $"{pe.Title} ({pe.StartDate})",
                        Value = $"{pe.Id.ToString()}"
                    });
                }
                else { BadRequest("No play-event added!"); }
            }
           
        }

        // GET: ChildrensPlayEvents/Details/5
        public async Task<IActionResult> Details(int? childId, int? playEventId)
        {
            if (childId == null || playEventId == null)
            {
                return NotFound();
            }

            var childrensPlayEvents = await _context.ChildrensPlayEvents
                .Include(c => c.Child)
                .Include(c => c.PlayEvent)
                .SingleOrDefaultAsync(m => m.ChildId == childId && m.PlayEventId == playEventId);
            if (childrensPlayEvents == null)
            {
                return NotFound();
            }

            return View(childrensPlayEvents);
        }

        // GET: ChildrensPlayEvents/Create
        public IActionResult Create()
        {
            //ViewData["ChildId"] = new SelectList(_context.Children, "Id", "Id");
            //ViewData["PlayEventId"] = new SelectList(_context.PlayEvents, "Id", "Id");

            var viewModel = new ChildrensPlayEventsViewModel();
            PopulatePlayEventDropDown(viewModel);
            PopulateChildrenDropDown(viewModel);
            return View(viewModel);        
        }

        // POST: ChildrensPlayEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChildId,PlayEventId")] ChildrensPlayEvents childrensPlayEvents)
        {          
            if (ModelState.IsValid)
            {
                _context.Add(childrensPlayEvents);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ChildId"] = new SelectList(_context.Children, "Id", "Id", childrensPlayEvents.ChildId);
            ViewData["PlayEventId"] = new SelectList(_context.PlayEvents, "Id", "Id", childrensPlayEvents.PlayEventId);
            return View(childrensPlayEvents);
        }

        //// GET: ChildrensPlayEvents/Edit/5
        //public async Task<IActionResult> Edit(int? childId, int? playEventId)
        //{
        //    if (childId == null || playEventId == null)
        //    {
        //        return NotFound();
        //    }

        //    var childrensPlayEvents = await _context.ChildrensPlayEvents.SingleOrDefaultAsync(m => m.ChildId == childId && m.PlayEventId == playEventId);
        //    if (childrensPlayEvents == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ChildId"] = new SelectList(_context.Children, "Id", "Id", childrensPlayEvents.ChildId);
        //    ViewData["PlayEventId"] = new SelectList(_context.PlayEvents, "Id", "Id", childrensPlayEvents.PlayEventId);
        //    return View(childrensPlayEvents);
        //}

        //// POST: ChildrensPlayEvents/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ChildId,PlayEventId")] ChildrensPlayEvents childrensPlayEvents)
        //{
        //    if (id != childrensPlayEvents.ChildId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(childrensPlayEvents);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ChildrensPlayEventsExists(childrensPlayEvents.ChildId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    ViewData["ChildId"] = new SelectList(_context.Children, "Id", "Id", childrensPlayEvents.ChildId);
        //    ViewData["PlayEventId"] = new SelectList(_context.PlayEvents, "Id", "Id", childrensPlayEvents.PlayEventId);
        //    return View(childrensPlayEvents);
        //}

        // GET: ChildrensPlayEvents/Delete/5
        public async Task<IActionResult> Delete(int? childId, int? playEventId)
        {
            if (childId == null || playEventId == null)
            {
                return NotFound();
            }

            var childrensPlayEvents = _context.ChildrensPlayEvents
                .Include(c => c.Child)
                .Include(c => c.PlayEvent)
                .SingleOrDefault(m => m.ChildId == childId && m.PlayEventId == playEventId);
            if (childrensPlayEvents == null)
            {
                return NotFound();
            }

            return View(childrensPlayEvents);
        }

        // POST: ChildrensPlayEvents/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int childId, int playEventId)
        {
            var childrensPlayEvents = await _context.ChildrensPlayEvents.SingleOrDefaultAsync(m => m.ChildId == childId && m.PlayEventId == playEventId);
            _context.ChildrensPlayEvents.Remove(childrensPlayEvents);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ChildrensPlayEventsExists(int childId)
        {
            return _context.ChildrensPlayEvents.Any(e => e.ChildId == childId);
        }
    }
}
