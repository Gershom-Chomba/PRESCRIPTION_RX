using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRESCRIPTIONS_RX.Models;

namespace PRESCRIPTIONS_RX.Controllers
{
    public class SoldDrugsController : Controller
    {
        private readonly PrescriptionRxDbContext _context;

        public SoldDrugsController(PrescriptionRxDbContext context)
        {
            _context = context;
        }

        // GET: SoldDrugs
        public async Task<IActionResult> Index()
        {
            var prescriptionRxDbContext = _context.SoldDrugs.Include(s => s.DrugTradeNameNavigation).Include(s => s.PharmacyNameNavigation);
            return View(await prescriptionRxDbContext.ToListAsync());
        }

        // GET: SoldDrugs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldDrug = await _context.SoldDrugs
                .Include(s => s.DrugTradeNameNavigation)
                .Include(s => s.PharmacyNameNavigation)
                .FirstOrDefaultAsync(m => m.SalesId == id);
            if (soldDrug == null)
            {
                return NotFound();
            }

            return View(soldDrug);
        }

        // GET: SoldDrugs/Create
        public IActionResult Create()
        {
            ViewData["DrugTradeName"] = new SelectList(_context.Drugs, "TradeName", "TradeName");
            ViewData["PharmacyName"] = new SelectList(_context.Pharmacies, "PharmacyName", "PharmacyName");
            return View();
        }

        // POST: SoldDrugs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesId,PharmacyName,DrugTradeName,DrugPrice")] SoldDrug soldDrug)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soldDrug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DrugTradeName"] = new SelectList(_context.Drugs, "TradeName", "TradeName", soldDrug.DrugTradeName);
            ViewData["PharmacyName"] = new SelectList(_context.Pharmacies, "PharmacyName", "PharmacyName", soldDrug.PharmacyName);
            return View(soldDrug);
        }

        // GET: SoldDrugs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldDrug = await _context.SoldDrugs.FindAsync(id);
            if (soldDrug == null)
            {
                return NotFound();
            }
            ViewData["DrugTradeName"] = new SelectList(_context.Drugs, "TradeName", "TradeName", soldDrug.DrugTradeName);
            ViewData["PharmacyName"] = new SelectList(_context.Pharmacies, "PharmacyName", "PharmacyName", soldDrug.PharmacyName);
            return View(soldDrug);
        }

        // POST: SoldDrugs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesId,PharmacyName,DrugTradeName,DrugPrice")] SoldDrug soldDrug)
        {
            if (id != soldDrug.SalesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soldDrug);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoldDrugExists(soldDrug.SalesId))
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
            ViewData["DrugTradeName"] = new SelectList(_context.Drugs, "TradeName", "TradeName", soldDrug.DrugTradeName);
            ViewData["PharmacyName"] = new SelectList(_context.Pharmacies, "PharmacyName", "PharmacyName", soldDrug.PharmacyName);
            return View(soldDrug);
        }

        // GET: SoldDrugs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldDrug = await _context.SoldDrugs
                .Include(s => s.DrugTradeNameNavigation)
                .Include(s => s.PharmacyNameNavigation)
                .FirstOrDefaultAsync(m => m.SalesId == id);
            if (soldDrug == null)
            {
                return NotFound();
            }

            return View(soldDrug);
        }

        // POST: SoldDrugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soldDrug = await _context.SoldDrugs.FindAsync(id);
            if (soldDrug != null)
            {
                _context.SoldDrugs.Remove(soldDrug);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoldDrugExists(int id)
        {
            return _context.SoldDrugs.Any(e => e.SalesId == id);
        }
    }
}
