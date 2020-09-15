using ClosedXML.Excel;
using EwidencjaWSK.Data;
using EwidencjaWSK.Models;
using EwidencjaWSK.Services;
using EwidencjaWSK.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EwidencjaWSK.Controllers
{
    [Authorize]
    public class RecordsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpContextAccessor _httpContext;

        public RecordsController(ApplicationDbContext context)
        {
            _context = context;
            //_httpContext = httpContext;
        }

        public async Task<IActionResult> Index(int page = 1, string search = "", int Year = 0)
        {
            int pageSize = 11;
            var applicationDbContext = new List<Record>();

            if (String.IsNullOrEmpty(search))
            {
                if (Year == 0)
                {
                    applicationDbContext = await _context.Records.Include(r => r.Supplier).ToListAsync();

                }
                else
                {
                    applicationDbContext = await _context.Records.Where(n => n.Date.Year == Year).ToListAsync();

                }
            }

            else // jak jest parametr do wyszukiwania
            {
                
                foreach (var item in _context.Records)
                {
                    if (item.Number.Contains(search))
                    {
                        applicationDbContext.Add(item);
                    }
                }

                if (applicationDbContext.Count == 0)
                {

                }
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

            recordsViewModel.Search = search;

            return View(recordsViewModel);

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

        public async Task<IActionResult> Reports()
        {
            var thisYear = DateTime.Now.Year;
            var allYears = new List<ReportsViewModel>();

            foreach (var item in _context.Records)
            {
                bool isNoted = false;

                foreach (var item2 in allYears)
                {
                    if (item.Date.Year == item2.Year)
                    { isNoted = true; }

                }

                if (isNoted == false)
                {
                    allYears.Add(new ReportsViewModel { Year = item.Date.Year });
                }
            }

            return View(allYears);
                
        }

        public async Task<IActionResult> PrintExcel(int Year = 0)
        {
            #region próba1
            //string path = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Host}:{HttpContext.Request.Host.Port}/Records/ReportsToPrint";
            //var render = new IronPdf.HtmlToPdf();
            //var doc = render.RenderUrlAsPdf(path);
            //doc.SaveAs($@"D:\htmlpfd.pdf");

            //return RedirectToAction("ReportsToPrint");
            #endregion

            //var exportToExcel = new ExportToExcel();
            //exportToExcel.MakeExcelSheet(await (_context.Records.Where(n => n.Date.Year == Year).ToListAsync()), await (_context.Suppliers.ToListAsync());

            var datatoExport = await(_context.Records.Where(n => n.Date.Year == Year).ToListAsync());
            var recordPart = await (_context.RecordsParts.ToListAsync());
            var recordAdditionalDoc = await (_context.RecordsAdditionalDocs.ToListAsync());
            var suppliers = await (_context.Suppliers.ToListAsync());
            var additionalDocs = await (_context.AdditionalDocs.ToListAsync());
            var parts = await (_context.Parts.ToListAsync());

            #region excel

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Raport");
                var currentRow = 1;
                var currentRecord = 0;

                worksheet.Cell(currentRow, 1).Value = "Ewidencja WSK dla " + Year.ToString() +" roku";
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = "Raport wygenerowano " + DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day;
                currentRow++;

                worksheet.Cell(currentRow, 1).Value = "Lp";
                worksheet.Cell(currentRow, 2).Value = "Numer kontraktu";
                worksheet.Cell(currentRow, 3).Value = "Data";
                worksheet.Cell(currentRow, 4).Value = "Wartość";
                worksheet.Cell(currentRow, 5).Value = "Waluta";
                worksheet.Cell(currentRow, 6).Value = "Dostawca";
                worksheet.Cell(currentRow, 7).Value = "Części";
                worksheet.Cell(currentRow, 8).Value = "Dokumenty";

                foreach (var record in datatoExport)
                {
                    currentRow++;
                    currentRecord++;
                    worksheet.Cell(currentRow, 1).Value = currentRecord;
                    worksheet.Cell(currentRow, 2).Value = record.Number;
                    worksheet.Cell(currentRow, 3).Value = record.Date;
                    worksheet.Cell(currentRow, 4).Value = record.Value;
                    worksheet.Cell(currentRow, 5).Value = record.Currency;
                    worksheet.Cell(currentRow, 6).Value = from supplierName in suppliers
                                                          where supplierName.SupplierId == record.SuplierId
                                                          select supplierName.Name;
                    var beginingRow = currentRow;

                    if (record.Parts != null)
                    {
                        
                        foreach (var part in recordPart)
                        {
                            if (part.RecordId == record.RecordId)
                            {
                                worksheet.Cell(currentRow, 7).Value = "Tutaj ma być część";
                                currentRow++;
                            }
                        }
                    }

                    if (record.AdditionalDocs != null)
                    {
                        currentRow = beginingRow;
                        
                        foreach (var doc in recordAdditionalDoc)
                        {
                            if (doc.RecordId == record.RecordId)
                            {
                                worksheet.Cell(currentRow, 8).Value = "Tutaj ma być dokumencik";
                                currentRow++;
                            }
                        }
                    }


                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Raport.xlsx");
                }
            }

            #endregion

            var routeValues = new RouteValueDictionary
            {
                {"Year", Year }
            };

            return RedirectToAction("ReportsToPrint", routeValues);

           
        }


        [HttpGet]
        public async Task<IActionResult> ReportsToPrint(int Year = 0)
        {
            var recordViewList = new RecordsViewModel();
            recordViewList.Records = await (_context.Records.Where(n => n.Date.Year == Year).ToListAsync());
            recordViewList.Suppliers = await (from supplier in _context.Suppliers
                                                select supplier).ToListAsync();
            recordViewList.Year = Year;
            
            return View(recordViewList);
        }
        
        public string GetBaseUrl()
        {
            var request = _httpContext.HttpContext.Request;
            var host = request.Host.ToUriComponent();
            var pathBase = request.PathBase.ToUriComponent();
            return $"{request.Scheme}://{host}{pathBase}";
        }


    }
}
