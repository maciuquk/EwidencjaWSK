using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EwidencjaWSK.Data;
using EwidencjaWSK.Models;
using EwidencjaWSK.ViewModel;

namespace EwidencjaWSK.Controllers
{
    public class AdditionalDocsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdditionalDocsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdditionalDocs
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 7;
            var additionalDocsViewModel = new AdditionalDocsViewModel();
            var applicationDbContext = (await _context.AdditionalDocs.ToListAsync());

            additionalDocsViewModel.AdditionalDocs = (applicationDbContext
                .OrderBy(p => p.AdditionalDocId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)).ToList();

            additionalDocsViewModel.PaginationViewModel = new PaginationViewModel
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = applicationDbContext.Count()
            };

            return View(additionalDocsViewModel);
        }

        // GET: AdditionalDocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalDoc = await _context.AdditionalDocs
                .FirstOrDefaultAsync(m => m.AdditionalDocId == id);
            if (additionalDoc == null)
            {
                return NotFound();
            }

            return View(additionalDoc);
        }

        // GET: AdditionalDocs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdditionalDocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdditionalDocId,Number,KindOfDoc,Date")] AdditionalDoc additionalDoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(additionalDoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(additionalDoc);
        }

        // GET: AdditionalDocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalDoc = await _context.AdditionalDocs.FindAsync(id);
            if (additionalDoc == null)
            {
                return NotFound();
            }
            return View(additionalDoc);
        }

        // POST: AdditionalDocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdditionalDocId,Number,KindOfDoc,Date")] AdditionalDoc additionalDoc)
        {
            if (id != additionalDoc.AdditionalDocId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(additionalDoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdditionalDocExists(additionalDoc.AdditionalDocId))
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
            return View(additionalDoc);
        }

        // GET: AdditionalDocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalDoc = await _context.AdditionalDocs
                .FirstOrDefaultAsync(m => m.AdditionalDocId == id);
            if (additionalDoc == null)
            {
                return NotFound();
            }

            return View(additionalDoc);
        }

        // POST: AdditionalDocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var additionalDoc = await _context.AdditionalDocs.FindAsync(id);
            _context.AdditionalDocs.Remove(additionalDoc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdditionalDocExists(int id)
        {
            return _context.AdditionalDocs.Any(e => e.AdditionalDocId == id);
        }
    }
}
