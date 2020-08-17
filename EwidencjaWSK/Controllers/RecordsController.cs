using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EwidencjaWSK.Data;
using EwidencjaWSK.Models;
using Microsoft.AspNetCore.Mvc;

namespace EwidencjaWSK.Controllers
{
    public class RecordsController : Controller
    {
        private ApplicationDbContext _context;

        public RecordsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Record> allRecords = new List<Record>();
            allRecords = _context.Records.ToList();
            return View(allRecords);
        }

        public IActionResult AddRecord()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddRecord(Record record)
        {
            _context.Add(record);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
