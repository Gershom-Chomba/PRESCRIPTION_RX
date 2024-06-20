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
    public class PharmaciesAddressesController : Controller
    {
        private readonly PrescriptionRxDbContext _context;

        public PharmaciesAddressesController(PrescriptionRxDbContext context)
        {
            _context = context;
        }

        // GET: PharmaciesAddresses
        public async Task<IActionResult> Index()
        {
            var prescriptionRxDbContext = _context.PharmaciesAddresses.Include(p => p.PharmacyNameNavigation);
            return View(await prescriptionRxDbContext.ToListAsync());
        }

        // GET: PharmaciesAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmaciesAddress = await _context.PharmaciesAddresses
                .Include(p => p.PharmacyNameNavigation)
                .FirstOrDefaultAsync(m => m.SN == id);
            if (pharmaciesAddress == null)
            {
                return NotFound();
            }

            return View(pharmaciesAddress);
        }

        // GET: PharmaciesAddresses/Create
        public IActionResult Create()
        {
            ViewData["PharmacyName"] = new SelectList(_context.Pharmacies, "PharmacyName", "PharmacyName");
            return View();
        }

        // POST: PharmaciesAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SN,PharmacyName,PlotNumber,Street,City")] PharmaciesAddress pharmaciesAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pharmaciesAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PharmacyName"] = new SelectList(_context.Pharmacies, "PharmacyName", "PharmacyName", pharmaciesAddress.PharmacyName);
            return View(pharmaciesAddress);
        }

        // GET: PharmaciesAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmaciesAddress = await _context.PharmaciesAddresses.FindAsync(id);
            if (pharmaciesAddress == null)
            {
                return NotFound();
            }
            ViewData["PharmacyName"] = new SelectList(_context.Pharmacies, "PharmacyName", "PharmacyName", pharmaciesAddress.PharmacyName);
            return View(pharmaciesAddress);
        }

        // POST: PharmaciesAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SN,PharmacyName,PlotNumber,Street,City")] PharmaciesAddress pharmaciesAddress)
        {
            if (id != pharmaciesAddress.SN)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pharmaciesAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PharmaciesAddressExists(pharmaciesAddress.SN))
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
            ViewData["PharmacyName"] = new SelectList(_context.Pharmacies, "PharmacyName", "PharmacyName", pharmaciesAddress.PharmacyName);
            return View(pharmaciesAddress);
        }

        // GET: PharmaciesAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmaciesAddress = await _context.PharmaciesAddresses
                .Include(p => p.PharmacyNameNavigation)
                .FirstOrDefaultAsync(m => m.SN == id);
            if (pharmaciesAddress == null)
            {
                return NotFound();
            }

            return View(pharmaciesAddress);
        }

        // POST: PharmaciesAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pharmaciesAddress = await _context.PharmaciesAddresses.FindAsync(id);
            if (pharmaciesAddress != null)
            {
                _context.PharmaciesAddresses.Remove(pharmaciesAddress);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PharmaciesAddressExists(int id)
        {
            return _context.PharmaciesAddresses.Any(e => e.SN == id);
        }
    }
}
