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
    public class TransactionController : Controller
    {
        private readonly ApplicationDbcontext _context;

        public TransactionController(ApplicationDbcontext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            var applicationDbcontext = _context.Transactions.Include(t => t.Category);
            return View(await applicationDbcontext.ToListAsync());
        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transaction/AddOrEdit
        public async Task<IActionResult> AddOrEdit(int Id=0)
        {
            populateCategories();

            if (Id == 0)
            {
                return View(new Transaction()); 
            }
            var transaction = await _context.Transactions.FindAsync(Id);

            if(transaction == null)
            {
                return NotFound();
            }

            return View(transaction );
        }

        // POST: Transaction/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,CategoryId,Amount,Note,Date")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                if(transaction.TransactionId == 0)
                {
                    _context.Add(transaction);

                }
                else
                {
                    _context.Update(transaction);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            populateCategories();

            return View(transaction);
        }




        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public void populateCategories()
        {
            var cateogirecollection = _context.categories.ToList();
            var Default = new Category() {CategoryId = 0 , Title="Choose a category" };
            cateogirecollection.Insert(0, Default);
            ViewBag.categories = cateogirecollection; 
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}
