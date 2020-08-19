using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EwidencjaWSK.Data;
using EwidencjaWSK.Models;
using EwidencjaWSK.ViewModel;
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
            //var ViewModel = new RecordsViewModel();
            //ViewModel.Records = _context.Records.ToList();
            //ViewModel.Suppliers = _context.Suppliers.ToList();
            //ViewModel.WarehouseDocs = _context.WarehouseDocs.ToList();
            //ViewModel.AccountDocs = _context.AccountDocs.ToList();
            //ViewModel.Parts = _context.Parts.ToList();

            List<Record> records = new List<Record>();
            records = _context.Records.ToList();
            return View(records);
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

        public IActionResult EditRecord(int id)
        {
            Record record = _context.Records.FirstOrDefault(n => n.RecordId == id);
            return View(record);
        }
    }
}
