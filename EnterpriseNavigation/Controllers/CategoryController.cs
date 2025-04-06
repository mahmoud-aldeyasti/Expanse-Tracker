using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnterpriseNavigation.Models;

namespace EnterpriseNavigation.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbcontext _context;

        public CategoryController(ApplicationDbcontext context)
        {
            _context = context;
        }

        // GET: Categoriy

        //Category/Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.categories.ToListAsync());
        }

        // GET: Categoriy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categoriy/AddOrEdit
        public async Task<IActionResult> AddOrEdit(int Id = 0)
        {
            if (Id == 0)
            {
                return View(new Category() );
            }
            var category = await _context.categories.FindAsync(Id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }

        // POST: Categoriy/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("CategoryId,Title,Icon,Type")] Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryId == 0)
                {
                    _context.Add(category);
                }
                else
                {
                    _context.Update(category);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }




        // POST: Categoriy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.categories.FindAsync(id);
            if (category != null)
            {
                _context.categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.categories.Any(e => e.CategoryId == id);
        }
    }
}
