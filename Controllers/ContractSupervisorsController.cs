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
    public class ContractSupervisorsController : Controller
    {
        private readonly PrescriptionRxDbContext _context;

        public ContractSupervisorsController(PrescriptionRxDbContext context)
        {
            _context = context;
        }

        // GET: ContractSupervisors
        public async Task<IActionResult> Index()
        {
            var prescriptionRxDbContext = _context.ContractSupervisors.Include(c => c.Contract).Include(c => c.PharmacyNameNavigation);
            return View(await prescriptionRxDbContext.ToListAsync());
        }

        // GET: ContractSupervisors/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractSupervisor = await _context.ContractSupervisors
                .Include(c => c.Contract)
                .Include(c => c.PharmacyNameNavigation)
                .FirstOrDefaultAsync(m => m.SupervisorSsn == id);
            if (contractSupervisor == null)
            {
                return NotFound();
            }

            return View(contractSupervisor);
        }

        // GET: ContractSupervisors/Create
        public IActionResult Create()
        {
            ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId");
            ViewData["PharmacyName"] = new SelectList(_context.Pharmacies, "PharmacyName", "PharmacyName");
            return View();
        }

        // POST: ContractSupervisors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SN,SupervisorSsn,FirstName,LastName,MiddleName,PlotNumber,Street,City,Gender,PhoneNumber,ContractId,PharmacyName")] ContractSupervisor contractSupervisor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contractSupervisor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId", contractSupervisor.ContractId);
            ViewData["PharmacyName"] = new SelectList(_context.Pharmacies, "PharmacyName", "PharmacyName", contractSupervisor.PharmacyName);
            return View(contractSupervisor);
        }

        // GET: ContractSupervisors/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractSupervisor = await _context.ContractSupervisors.FindAsync(id);
            if (contractSupervisor == null)
            {
                return NotFound();
            }
            ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId", contractSupervisor.ContractId);
            ViewData["PharmacyName"] = new SelectList(_context.Pharmacies, "PharmacyName", "PharmacyName", contractSupervisor.PharmacyName);
            return View(contractSupervisor);
        }

        // POST: ContractSupervisors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SN,SupervisorSsn,FirstName,LastName,MiddleName,PlotNumber,Street,City,Gender,PhoneNumber,ContractId,PharmacyName")] ContractSupervisor contractSupervisor)
        {
            if (id != contractSupervisor.SupervisorSsn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contractSupervisor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractSupervisorExists(contractSupervisor.SupervisorSsn))
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
            ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId", contractSupervisor.ContractId);
            ViewData["PharmacyName"] = new SelectList(_context.Pharmacies, "PharmacyName", "PharmacyName", contractSupervisor.PharmacyName);
            return View(contractSupervisor);
        }

        // GET: ContractSupervisors/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractSupervisor = await _context.ContractSupervisors
                .Include(c => c.Contract)
                .Include(c => c.PharmacyNameNavigation)
                .FirstOrDefaultAsync(m => m.SupervisorSsn == id);
            if (contractSupervisor == null)
            {
                return NotFound();
            }

            return View(contractSupervisor);
        }

        // POST: ContractSupervisors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var contractSupervisor = await _context.ContractSupervisors.FindAsync(id);
            if (contractSupervisor != null)
            {
                _context.ContractSupervisors.Remove(contractSupervisor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractSupervisorExists(string id)
        {
            return _context.ContractSupervisors.Any(e => e.SupervisorSsn == id);
        }
    }
}
