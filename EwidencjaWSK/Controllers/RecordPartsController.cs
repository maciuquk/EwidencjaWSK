using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EwidencjaWSK.Data;
using EwidencjaWSK.Models;

namespace EwidencjaWSK.Controllers
{
    public class RecordPartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecordPartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RecordParts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RecordsParts.Include(r => r.Part).Include(r => r.Record);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RecordParts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recordPart = await _context.RecordsParts
                .Include(r => r.Part)
                .Include(r => r.Record)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (recordPart == null)
            {
                return NotFound();
            }

            return View(recordPart);
        }

        // GET: RecordParts/Create
        public IActionResult Create()
        {
            ViewData["PartId"] = new SelectList(_context.Parts, "PartId", "PartId");
            ViewData["RecordId"] = new SelectList(_context.Records, "RecordId", "Number");
            return View();
        }

        // POST: RecordParts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordId,PartId")] RecordPart recordPart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recordPart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PartId"] = new SelectList(_context.Parts, "PartId", "PartId", recordPart.PartId);
            ViewData["RecordId"] = new SelectList(_context.Records, "RecordId", "Number", recordPart.RecordId);
            return View(recordPart);
        }

        // GET: RecordParts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recordPart = await _context.RecordsParts.FindAsync(id);
            if (recordPart == null)
            {
                return NotFound();
            }
            ViewData["PartId"] = new SelectList(_context.Parts, "PartId", "PartId", recordPart.PartId);
            ViewData["RecordId"] = new SelectList(_context.Records, "RecordId", "Number", recordPart.RecordId);
            return View(recordPart);
        }

        // POST: RecordParts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecordId,PartId")] RecordPart recordPart)
        {
            if (id != recordPart.RecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recordPart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordPartExists(recordPart.RecordId))
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
            ViewData["PartId"] = new SelectList(_context.Parts, "PartId", "PartId", recordPart.PartId);
            ViewData["RecordId"] = new SelectList(_context.Records, "RecordId", "Number", recordPart.RecordId);
            return View(recordPart);
        }

        // GET: RecordParts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recordPart = await _context.RecordsParts
                .Include(r => r.Part)
                .Include(r => r.Record)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (recordPart == null)
            {
                return NotFound();
            }

            return View(recordPart);
        }

        // POST: RecordParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recordPart = await _context.RecordsParts.FindAsync(id);
            _context.RecordsParts.Remove(recordPart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordPartExists(int id)
        {
            return _context.RecordsParts.Any(e => e.RecordId == id);
        }
    }
}
