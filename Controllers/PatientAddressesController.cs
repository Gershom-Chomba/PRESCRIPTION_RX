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
    public class PatientAddressesController : Controller
    {
        private readonly PrescriptionRxDbContext _context;

        public PatientAddressesController(PrescriptionRxDbContext context)
        {
            _context = context;
        }

        // GET: PatientAddresses
        public async Task<IActionResult> Index()
        {
            var prescriptionRxDbContext = _context.PatientAddresses.Include(p => p.PatientSsnNavigation);
            return View(await prescriptionRxDbContext.ToListAsync());
        }

        // GET: PatientAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientAddress = await _context.PatientAddresses
                .Include(p => p.PatientSsnNavigation)
                .FirstOrDefaultAsync(m => m.SN == id);
            if (patientAddress == null)
            {
                return NotFound();
            }

            return View(patientAddress);
        }

        // GET: PatientAddresses/Create
        public IActionResult Create()
        {
            ViewData["PatientSsn"] = new SelectList(_context.Patients, "PatientSsn", "PatientSsn");
            return View();
        }

        // POST: PatientAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SN,PatientSsn,PlotNumber,Street,City")] PatientAddress patientAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientSsn"] = new SelectList(_context.Patients, "PatientSsn", "PatientSsn", patientAddress.PatientSsn);
            return View(patientAddress);
        }

        // GET: PatientAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientAddress = await _context.PatientAddresses.FindAsync(id);
            if (patientAddress == null)
            {
                return NotFound();
            }
            ViewData["PatientSsn"] = new SelectList(_context.Patients, "PatientSsn", "PatientSsn", patientAddress.PatientSsn);
            return View(patientAddress);
        }

        // POST: PatientAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SN,PatientSsn,PlotNumber,Street,City")] PatientAddress patientAddress)
        {
            if (id != patientAddress.SN)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientAddressExists(patientAddress.SN))
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
            ViewData["PatientSsn"] = new SelectList(_context.Patients, "PatientSsn", "PatientSsn", patientAddress.PatientSsn);
            return View(patientAddress);
        }

        // GET: PatientAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientAddress = await _context.PatientAddresses
                .Include(p => p.PatientSsnNavigation)
                .FirstOrDefaultAsync(m => m.SN == id);
            if (patientAddress == null)
            {
                return NotFound();
            }

            return View(patientAddress);
        }

        // POST: PatientAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patientAddress = await _context.PatientAddresses.FindAsync(id);
            if (patientAddress != null)
            {
                _context.PatientAddresses.Remove(patientAddress);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientAddressExists(int id)
        {
            return _context.PatientAddresses.Any(e => e.SN == id);
        }
    }
}
