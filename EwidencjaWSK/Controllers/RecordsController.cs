using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EwidencjaWSK.Data;
using EwidencjaWSK.Models;
using Microsoft.AspNetCore.Routing;
using EwidencjaWSK.ViewModel;

namespace EwidencjaWSK.Controllers
{
    public class RecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 7;

            int Year = 0;
            var applicationDbContext = new List<Record>();

            if (Year == 0)
            {
                applicationDbContext = await _context.Records.Include(r => r.Supplier).ToListAsync();

            }
            else
            {
                applicationDbContext = await _context.Records.Where(n => n.Date.Year == Year).ToListAsync();

            }

            var recordsViewModel = new RecordsViewModel();
            recordsViewModel.Records = applicationDbContext;
            recordsViewModel.Suppliers = await (from supplier in _context.Suppliers
                                                select supplier).ToListAsync();

            
            recordsViewModel.Records = (applicationDbContext
                .OrderBy(p => p.RecordId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)).ToList();

            recordsViewModel.PaginationViewModel = new PaginationViewModel
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = applicationDbContext.Count()
            };

            return View(recordsViewModel);
            //return View(applicationDbContext);

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = await _context.Records
                .Include(r => r.Supplier)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (record == null)
            {
                return NotFound();
            }

            var recordViewModel = new RecordViewModel();
            recordViewModel.Suppliers = await (from supplier in _context.Suppliers
                                               select supplier).ToListAsync();
            recordViewModel.Record = record;

            var docs = new List<AdditionalDoc>();
            foreach (var item in _context.AdditionalDocs)
            {
                foreach (var item2 in _context.RecordsAdditionalDocs)
                {
                    if ((item.AdditionalDocId == item2.AdditionalDocId) && (item2.RecordId == record.RecordId))
                    {
                        docs.Add(item);
                    }
                }
            }
            recordViewModel.AdditionalDocs = docs;
            
            var parts = new List<Part>();
            foreach (var item in _context.Parts)
            {
                foreach (var item2 in _context.RecordsParts)
                {
                    if ((item.PartId == item2.PartId) && (item2.RecordId == record.RecordId))
                    {
                        parts.Add(item);
                    }
                }
            }
            recordViewModel.Parts = parts;




            return View(recordViewModel);
        }

        public IActionResult Create()
        {
            ViewData["SuplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordId,Number,Date,Value,Currency,Description,KindOfTransaction,IsCheckDenyList,IsCheckWarningSignalList,IsInPartsBase,IsNecessaryMinistryPermit,SuplierId")] Record record)
        {
            if (ModelState.IsValid)
            {
                _context.Add(record);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SuplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId", record.SuplierId);
            return View(record);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = await _context.Records.FindAsync(id);
            if (record == null)
            {
                return NotFound();
            }

            var recordViewModel = new RecordViewModel();
            recordViewModel.Suppliers = await (from supplier in _context.Suppliers
                                               select supplier).ToListAsync();
            recordViewModel.Record = record;

            ViewData["SuplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Name", record.SuplierId);
            return View(recordViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecordId,Number,Date,Value,Currency,Description,KindOfTransaction,IsCheckDenyList,IsCheckWarningSignalList,IsInPartsBase,IsNecessaryMinistryPermit,SuplierId")] Record record)
        {
            if (id != record.RecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(record);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordExists(record.RecordId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SuplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId", record.SuplierId);
            return View(record);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = await _context.Records
                .Include(r => r.Supplier)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (record == null)
            {
                return NotFound();
            }

            return View(record);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var record = await _context.Records.FindAsync(id);
            _context.Records.Remove(record);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordExists(int id)
        {
            return _context.Records.Any(e => e.RecordId == id);
        }

        public async Task<IActionResult> ThisYearRecords()
        {
            int thisYear = DateTime.Now.Year;
            var routeValues = new RouteValueDictionary
            {
                { "Year", thisYear}
            };

            return RedirectToAction("Index", routeValues);
        }

        public async Task<IActionResult> LastYearRecords()
        {
            int lastYear = DateTime.Now.Year - 1;
            var routeValues = new RouteValueDictionary
            {
                { "Year", lastYear}
            };

            return RedirectToAction("Index", routeValues);
        }
    }
}
