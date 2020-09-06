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
using Microsoft.AspNetCore.Authorization;

namespace EwidencjaWSK.Controllers
{
    [Authorize]
    public class RecordAdditionalDocsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecordAdditionalDocsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RecordAdditionalDocs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RecordsAdditionalDocs.Include(r => r.AdditionalDoc).Include(r => r.Record);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RecordAdditionalDocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recordAdditionalDoc = await _context.RecordsAdditionalDocs
                .Include(r => r.AdditionalDoc)
                .Include(r => r.Record)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (recordAdditionalDoc == null)
            {
                return NotFound();
            }

            return View(recordAdditionalDoc);
        }

        // GET: RecordAdditionalDocs/Create
        public IActionResult Create(int id)
        {

            //ViewData["AdditionalDocId"] = new SelectList(_context.AdditionalDocs, "AdditionalDocId", "AdditionalDocId");
            //ViewData["RecordId"] = new SelectList(_context.Records, "RecordId", "Number");

            var recordAdditionalDocViewModel = new RecordAdditionalDocViewModel();
            recordAdditionalDocViewModel.Record = _context.Records.Where(n => n.RecordId == id).SingleOrDefault();
            recordAdditionalDocViewModel.AdditionalDocs = new List<AdditionalDoc>();


            foreach (var additionalDoc in _context.AdditionalDocs)
            {
                bool isExist = false;

                foreach (var recordAdditionalDoc in _context.RecordsAdditionalDocs)
                {
                    if ((recordAdditionalDoc.RecordId == recordAdditionalDocViewModel.Record.RecordId) && (recordAdditionalDoc.AdditionalDocId == additionalDoc.AdditionalDocId))
                    {
                        isExist = true;
                        continue;

                    }
                }
                if (!isExist)
                {
                    recordAdditionalDocViewModel.AdditionalDocs.Add(additionalDoc);
                    isExist = false;
                }
            }

            if (recordAdditionalDocViewModel.AdditionalDocs.Count() == 0)
            {
                var routeValues = new RouteValueDictionary
                {
                    { "id", id }
                };
                return RedirectToAction("Details", "Records", routeValues);
            }

            return View(recordAdditionalDocViewModel);
        }

        // POST: RecordAdditionalDocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int RecordId, int AdditionalDocId)
        {
            var recordAdditionalDoc = new RecordAdditionalDoc();
            recordAdditionalDoc.RecordId = RecordId;
            recordAdditionalDoc.AdditionalDocId= AdditionalDocId;
            
            var routeValues = new RouteValueDictionary
                {
                    { "id", RecordId }
                };


            if (ModelState.IsValid)
            {
                _context.Add(recordAdditionalDoc);
                await _context.SaveChangesAsync();


                return RedirectToAction("Details", "Records", routeValues);
            }

            return RedirectToAction("Details", "Records", routeValues);

            //if (ModelState.IsValid)
            //{
            //    _context.Add(recordAdditionalDoc);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["AdditionalDocId"] = new SelectList(_context.AdditionalDocs, "AdditionalDocId", "AdditionalDocId", recordAdditionalDoc.AdditionalDocId);
            //ViewData["RecordId"] = new SelectList(_context.Records, "RecordId", "Number", recordAdditionalDoc.RecordId);
            //return View(recordAdditionalDoc);
        }

        // GET: RecordAdditionalDocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recordAdditionalDoc = await _context.RecordsAdditionalDocs.FindAsync(id);
            if (recordAdditionalDoc == null)
            {
                return NotFound();
            }
            ViewData["AdditionalDocId"] = new SelectList(_context.AdditionalDocs, "AdditionalDocId", "AdditionalDocId", recordAdditionalDoc.AdditionalDocId);
            ViewData["RecordId"] = new SelectList(_context.Records, "RecordId", "Number", recordAdditionalDoc.RecordId);
            return View(recordAdditionalDoc);
        }

        // POST: RecordAdditionalDocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("RecordId,AdditionalDocId")] RecordAdditionalDoc recordAdditionalDoc)
        //{
        //    if (id != recordAdditionalDoc.RecordId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(recordAdditionalDoc);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!RecordAdditionalDocExists(recordAdditionalDoc.RecordId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["AdditionalDocId"] = new SelectList(_context.AdditionalDocs, "AdditionalDocId", "AdditionalDocId", recordAdditionalDoc.AdditionalDocId);
        //    ViewData["RecordId"] = new SelectList(_context.Records, "RecordId", "Number", recordAdditionalDoc.RecordId);
        //    return View(recordAdditionalDoc);
        //}


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int RecordId, int AdditionalDocId)
        {

            foreach (var recordAdditionaDoc in _context.RecordsAdditionalDocs)
            {
                if ((recordAdditionaDoc.RecordId == RecordId) && (recordAdditionaDoc.AdditionalDocId == AdditionalDocId))
                {
                    _context.RecordsAdditionalDocs.Remove(recordAdditionaDoc);
                }
            }

            await _context.SaveChangesAsync();


            var routeValues = new RouteValueDictionary
            {
                {"id", RecordId }
            };

            return RedirectToAction("Details", "Records", routeValues);
        }

        // POST: RecordAdditionalDocs/Delete/5
        
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var recordAdditionalDoc = await _context.RecordsAdditionalDocs.FindAsync(id);
        //    _context.RecordsAdditionalDocs.Remove(recordAdditionalDoc);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool RecordAdditionalDocExists(int id)
        //{
        //    return _context.RecordsAdditionalDocs.Any(e => e.RecordId == id);
        //}
    }
}
