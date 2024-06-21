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
    public class ContractsController : Controller
    {
        private readonly PrescriptionRxDbContext _context;

        public ContractsController(PrescriptionRxDbContext context)
        {
            _context = context;
        }

        // GET: Contracts
        public async Task<IActionResult> Index()
        {
            var prescriptionRxDbContext = _context.Contracts.Include(c => c.PharmaceuticalNameNavigation).Include(c => c.PharmacyNameNavigation);
            return View(await prescriptionRxDbContext.ToListAsync());
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.PharmaceuticalNameNavigation)
                .Include(c => c.PharmacyNameNavigation)
                .FirstOrDefaultAsync(m => m.ContractId == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            ViewData["PharmaceuticalName"] = new SelectList(_context.PharmaceuticalCompanies, "CompanyName", "CompanyName");
            ViewData["PharmacyName"] = new SelectList(_context.Pharmacies, "PharmacyName", "PharmacyName");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractId,Startdate,Enddate,ContractText,PharmacyName,PharmaceuticalName")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PharmaceuticalName"] = new SelectList(_context.PharmaceuticalCompanies, "CompanyName", "CompanyName", contract.PharmaceuticalName);
            ViewData["PharmacyName"] = new SelectList(_context.Pharmacies, "PharmacyName", "PharmacyName", contract.PharmacyName);
            return View(contract);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            ViewData["PharmaceuticalName"] = new SelectList(_context.PharmaceuticalCompanies, "CompanyName", "CompanyName", contract.PharmaceuticalName);
            ViewData["PharmacyName"] = new SelectList(_context.Pharmacies, "PharmacyName", "PharmacyName", contract.PharmacyName);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContractId,Startdate,Enddate,ContractText,PharmacyName,PharmaceuticalName")] Contract contract)
        {
            if (id != contract.ContractId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.ContractId))
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
            ViewData["PharmaceuticalName"] = new SelectList(_context.PharmaceuticalCompanies, "CompanyName", "CompanyName", contract.PharmaceuticalName);
            ViewData["PharmacyName"] = new SelectList(_context.Pharmacies, "PharmacyName", "PharmacyName", contract.PharmacyName);
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.PharmaceuticalNameNavigation)
                .Include(c => c.PharmacyNameNavigation)
                .FirstOrDefaultAsync(m => m.ContractId == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract != null)
            {
                _context.Contracts.Remove(contract);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(e => e.ContractId == id);
        }
    }
}
