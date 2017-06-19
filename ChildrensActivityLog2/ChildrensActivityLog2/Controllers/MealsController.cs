using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChildrensActivityLog2.Repositories;
using ChildrensActivityLog2.ViewModels;
using System.Collections.Generic;
using ChildrensActivityLog2.Models;

namespace ChildrensActivityLog2.Controllers
{
    public class MealsController : Controller
    {
        private readonly ChildrensActivityLogContext _context;

        public MealsController(ChildrensActivityLogContext context)
        {
            _context = context;    
        }
        private void PopulateChildrenDropDown(MealsCreateViewModel viewModel)
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
        // GET: Meals
        public async Task<IActionResult> Index()
        {
            var childrensActivityLogContext = _context.Meals.Include(m => m.Child);
            return View(await childrensActivityLogContext.ToListAsync());
        }

        // GET: Meals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals
                .Include(m => m.Child)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // GET: Meals/Create
        public IActionResult Create()
        {
            //ViewData["ChildId"] = new SelectList(_context.Children, "Id", "Id");
            var viewModel = new MealsCreateViewModel();
            PopulateChildrenDropDown(viewModel);
            return View(viewModel);
        }

        // POST: Meals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ChildId,From,To,ToEat,ToDrink")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meal);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ChildId"] = new SelectList(_context.Children, "Id", "Id", meal.ChildId);
            return View(meal);
        }

        // GET: Meals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals.SingleOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }
            ViewData["ChildId"] = new SelectList(_context.Children, "Id", "Id", meal.ChildId);
            return View(meal);
        }

        // POST: Meals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChildId,From,To,ToEat,ToDrink")] Meal meal)
        {
            if (id != meal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealExists(meal.Id))
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
            ViewData["ChildId"] = new SelectList(_context.Children, "Id", "Id", meal.ChildId);
            return View(meal);
        }

        // GET: Meals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals
                .Include(m => m.Child)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meal = await _context.Meals.SingleOrDefaultAsync(m => m.Id == id);
            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MealExists(int id)
        {
            return _context.Meals.Any(e => e.Id == id);
        }
    }
}
