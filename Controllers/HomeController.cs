using Microsoft.AspNetCore.Mvc;

namespace MyCvApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult About() => View();

        public IActionResult CV() => View();

        public IActionResult Projects() => View();

        [HttpGet]
        public IActionResult TaxCalculator() => View();

        [HttpPost]
        public IActionResult TaxCalculator(decimal income)
        {
            decimal tax = CalculateNigerianTax(income);
            ViewBag.Tax = tax;
            ViewBag.Income = income;
            return View();
        }

        private decimal CalculateNigerianTax(decimal income)
        {
            if (income <= 300000) return 0;
            if (income <= 600000) return (income - 300000) * 0.07m;
            if (income <= 1100000) return (300000 * 0.07m) + (income - 600000) * 0.11m;
            return (300000 * 0.07m) + (500000 * 0.11m) + (income - 1100000) * 0.15m;
        }
    }
}