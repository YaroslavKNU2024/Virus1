#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewVirusApp;

namespace NewVirusApp.Controllers
{
    public class VariantsController : Controller
    {
        private readonly VirusBaseContext _context;

        public VariantsController(VirusBaseContext context)
        {
            _context = context;
        }

        // GET: Variants
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null || name == null) {
                var variants = _context.Variants.Include(x => x.Virus);
                return View(await variants.ToListAsync());
            }

            //Знаходження кафедр за факультетами
            ViewBag.VirusId = id;
            ViewBag.VirusName = name;
            var virusesByGroup = _context.Variants.Where(x => x.VirusId == id).Include(x => x.Virus);
            return View(await virusesByGroup.ToListAsync());
        }

        // GET: Variants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variant = await _context.Variants
                .Include(v => v.Virus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (variant == null)
            {
                return NotFound();
            }

            return View(variant);
        }

        // GET: Variants/Create
        public IActionResult Create(int? virusId)
        {
            if (virusId == null) return View();
            ViewData["VirusId"] = new SelectList(_context.Viruses, "Id", "VirusId");
            ViewBag.VirusId = virusId;
            ViewBag.VirusName = _context.Viruses.Where(f => f.Id == virusId).FirstOrDefault().VirusName;
            ViewBag.VirusId = virusId;
            ViewBag.VirusName = _context.Viruses.Where(v => v.Id == virusId).FirstOrDefault().VirusName;
            var virus = _context.Viruses.Where(v => v.Id == virusId).FirstOrDefault();
            var variantlist = virus.Variants.ToList();
            var countrieslist = new List<string>();
            foreach (var v in variantlist) {
                foreach (var d in v.CountriesVariants)
                    countrieslist.Add(d.Country.CountryName);
            }
                //ViewData["VirusId"] = new SelectList(_context.Viruses, "Id", "Id");
                return View();
        }

        // POST: Variants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? virusId, [Bind("Id,VariantName,VariantOrigin,VariantDateDiscovered,VirusId")] Variant variant)
        {
            if (virusId == null)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(variant);
                    await _context.SaveChangesAsync();
                    var countrieslist = new List<string>();
                        foreach (var d in variant.CountriesVariants)
                            countrieslist.Add(d.Country.CountryName);
                    
                    return RedirectToAction(nameof(Index));
                }
                return View(variant);
            }
            variant.VirusId = virusId;
            variant.Virus = await _context.Viruses.FindAsync(variant.VirusId);
            ModelState.ClearValidationState(nameof(variant.Virus));
            TryValidateModel(variant.Virus, nameof(variant.Virus));
            if (ModelState.IsValid)
            {
                _context.Add(variant);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Variants", new { id = virusId, name = _context.VirusGroups.Where(c => c.Id == virusId).FirstOrDefault().GroupName });
            }
            //ViewData["GroupId"] = new SelectList(_context.VirusGroups, "Id", "GroupName", virus.GroupId);
            //return View(virus);
            return RedirectToAction("Index", "Variants", new { id = virusId, name = _context.VirusGroups.Where(c => c.Id == virusId).FirstOrDefault().GroupName });

            //if (ModelState.IsValid)
            //{
            //    _context.Add(variant);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["VirusId"] = new SelectList(_context.Viruses, "Id", "Id", variant.VirusId);
            //return View(variant);
        }

        // GET: Variants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variant = await _context.Variants.FindAsync(id);
            if (variant == null)
            {
                return NotFound();
            }
            ViewData["VirusId"] = new SelectList(_context.Viruses, "Id", "Id", variant.VirusId);
            return View(variant);
        }

        // POST: Variants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VariantName,VariantOrigin,VariantDateDiscovered,VirusId")] Variant variant)
        {
            if (id != variant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(variant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VariantExists(variant.Id))
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
            ViewData["VirusId"] = new SelectList(_context.Viruses, "Id", "Id", variant.VirusId);
            return View(variant);
        }

        // GET: Variants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variant = await _context.Variants
                .Include(v => v.Virus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (variant == null)
            {
                return NotFound();
            }

            return View(variant);
        }

        // POST: Variants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var variant = await _context.Variants.FindAsync(id);
            _context.Variants.Remove(variant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VariantExists(int id)
        {
            return _context.Variants.Any(e => e.Id == id);
        }
    }
}
