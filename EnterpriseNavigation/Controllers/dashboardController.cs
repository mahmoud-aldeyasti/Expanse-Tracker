using EnterpriseNavigation.Migrations;
using EnterpriseNavigation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EnterpriseNavigation.Controllers
{
    public class dashboardController : Controller
    {
        private readonly ApplicationDbcontext _context;

        public dashboardController(ApplicationDbcontext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            // last week transaction 

            var StartDate = DateTime.Today.AddDays(-7);

            var EndDate = DateTime.Today;

            var SelectedTransactions = await _context.Transactions
                .Include(t => t.Category).
                Where(t => t.Date >= StartDate && t.Date <= EndDate).
                ToListAsync(); 

             var Income = SelectedTransactions
                .Where( t => t.Category?.Type == "Income")
                .Sum( t=> t.Amount) ;

            ViewBag.Income = Income.ToString("c0"); 

             var Expense  = SelectedTransactions
                .Where(t => t.Category?.Type == "Expense").
                Sum(t => t.Amount) ;

            ViewBag.Expense = Expense.ToString("c0");

            var Balance =  Income - Expense ;

            var culture = new CultureInfo("en-us"); 

            culture.NumberFormat.CurrencyNegativePattern = 1;

           

            ViewBag.Balance = string.Format(culture, "{0:C0}", Balance);


            //Expense by category 
            ViewBag.Expensbycategory = SelectedTransactions.Where(t => t.Category?.Type == "Expense")
                .GroupBy(t => t.Category.CategoryId)
                .Select(g => new
                {
                    
                    titleWithIcon = g.First().Category.Icon +" " + g.First().Category.Title,
                    Amount= g.Sum(t => t.Amount),
                    formatedAmount = g.Sum(t => t.Amount).ToString("c0")
                }).OrderByDescending( a => a.Amount)

                .ToList();



            return View();
        }
    }
}
