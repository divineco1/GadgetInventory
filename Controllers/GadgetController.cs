using Microsoft.AspNetCore.Mvc;
using GadgetInventory.Models;
using System.Collections.Generic;

namespace GadgetInventory.Controllers
{
    public class GadgetController : Controller
    {
        // Static in-memory list so data stays while the app is running
        private static List<Gadget> _gadgets = new List<Gadget>
        {
            new Gadget { Id = 1, Brand = "Apple", ModelName = "iPhone 15", Price = 799.00m },
            new Gadget { Id = 2, Brand = "Samsung", ModelName = "Galaxy S24", Price = 849.00m }
        };

        // GET: /Gadget or /Gadget/Index
        public IActionResult Index()
        {
            return View(_gadgets);
        }

        // GET: /Gadget/Create (Shows the blank form)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Gadget/Create (Receives and validates submitted form data)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Gadget newGadget)
        {
            if (!ModelState.IsValid)
            {
                // Re-show the form with validation messages if any rule failed
                return View(newGadget);
            }

            newGadget.Id = _gadgets.Count + 1;
            _gadgets.Add(newGadget);

            // Post-Redirect-Get (PRG) pattern prevents duplicate entries on refresh
            return RedirectToAction(nameof(Index));
        }
    }
}
