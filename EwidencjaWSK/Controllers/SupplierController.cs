using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EwidencjaWSK.Data;
using EwidencjaWSK.Models;
using Microsoft.AspNetCore.Mvc;

namespace EwidencjaWSK.Controllers
{
    public class SupplierController : Controller
    {
        private ApplicationDbContext _context;

        public SupplierController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Supplier> suppliers = new List<Supplier>();
            suppliers = _context.Suppliers.ToList();
            return View(suppliers);
        }

        public IActionResult AddSupplier()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSupplier(Supplier supplier)
        {
            _context.Add(supplier);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
