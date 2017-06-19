using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChildrensActivityLog2.Models;
using ChildrensActivityLog2.Repositories;
using ChildrensActivityLog2.ViewModels;

namespace ChildrensActivityLog2.Controllers
{
    public class ChildrenController : Controller
    {
        private readonly ChildrensActivityLogContext _context;

        public ChildrenController(ChildrensActivityLogContext context)
        {
            _context = context;    
        }

        // GET: Children
        public async Task<IActionResult> Index()
        {
            var viewModel = new ChildCreateViewModel();
            viewModel.Child = await _context.Children.ToListAsync();
            return View(viewModel);
        }

        // GET: Children/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Children
                .SingleOrDefaultAsync(m => m.Id == id);
            if (child == null)
            {
                return NotFound();
            }
            child.ChildrensPlayEvents = _context.ChildrensPlayEvents.Include(p => p.PlayEvent).Where(pe => pe.ChildId == id).ToList();
            child.Meals = _context.Meals.Where(m => m.ChildId == id).OrderBy(p => p.From).Take(10).ToList();
            child.SleepingPeriods = _context.SleepingPeriods.Where(s => s.ChildId == id).OrderBy(p => p.From).Take(10).ToList();
            var viewModel = new ChildDetailsViewModel() {
                Id = child.Id,                
                DateOfBirth = child.DateOfBirth,
                FirstName = child.FirstName,
                LastName = child.LastName,
                Meals = child.Meals,
                SleepingPeriods = child.SleepingPeriods,
                ChildrensPlayEvents = child.ChildrensPlayEvents,
                PlayEvents = child.ChildrensPlayEvents.Select(p => p.PlayEvent).OrderBy(p => p.StartDate).Take(10).ToList()
            };

            return View(viewModel);
        }

        // GET: Children/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Children/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,DateOfBirth")] Child child)
        {
            if (ModelState.IsValid)
            {
                _context.Add(child);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
           
            return View(child);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Children.SingleOrDefaultAsync(m => m.Id == id);
            if (child == null)
            {
                return NotFound();
            }
            var viewModel = new ChildCreateViewModel() {
                Id = child.Id,
                DateOfBirth = child.DateOfBirth,
                FirstName = child.FirstName,
                LastName = child.LastName};
            return View(viewModel);
        }

        // POST: Children/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,DateOfBirth")] Child child)
        {
            if (id != child.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(child);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChildExists(child.Id))
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
            var viewModel = new ChildCreateViewModel()
            {
                FirstName = child.FirstName,
                LastName = child.LastName,
                DateOfBirth = child.DateOfBirth
            };
            return View(viewModel);
          
        }

        // GET: Children/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Children
                .SingleOrDefaultAsync(m => m.Id == id);
            if (child == null)
            {
                return NotFound();
            }

            return View(child);
        }

        // POST: Children/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var child = await _context.Children.SingleOrDefaultAsync(m => m.Id == id);
            _context.Children.Remove(child);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ChildExists(int id)
        {
            return _context.Children.Any(e => e.Id == id);
        }
    }
}
