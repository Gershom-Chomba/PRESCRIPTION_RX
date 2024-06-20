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
    public class PrescriptionsController : Controller
    {
        private readonly PrescriptionRxDbContext _context;

        public PrescriptionsController(PrescriptionRxDbContext context)
        {
            _context = context;
        }

        // GET: Prescriptions
        public async Task<IActionResult> Index()
        {
            var prescriptionRxDbContext = _context.Prescriptions.Include(p => p.DrugTradeNameNavigation).Include(p => p.PatientSsnNavigation).Include(p => p.PrescribingPhysicianNavigation);
            return View(await prescriptionRxDbContext.ToListAsync());
        }

        // GET: Prescriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescriptions
                .Include(p => p.DrugTradeNameNavigation)
                .Include(p => p.PatientSsnNavigation)
                .Include(p => p.PrescribingPhysicianNavigation)
                .FirstOrDefaultAsync(m => m.PresciptionId == id);
            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }

        // GET: Prescriptions/Create
        public IActionResult Create()
        {
            ViewData["DrugTradeName"] = new SelectList(_context.Drugs, "TradeName", "TradeName");
            ViewData["PatientSsn"] = new SelectList(_context.Patients, "PatientSsn", "PatientSsn");
            ViewData["PrescribingPhysician"] = new SelectList(_context.Doctors, "DoctorsSsn", "DoctorsSsn");
            return View();
        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PresciptionId,PatientSsn,PrescribingPhysician,PrescriptionDate,Quantity,DrugTradeName")] Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prescription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DrugTradeName"] = new SelectList(_context.Drugs, "TradeName", "TradeName", prescription.DrugTradeName);
            ViewData["PatientSsn"] = new SelectList(_context.Patients, "PatientSsn", "PatientSsn", prescription.PatientSsn);
            ViewData["PrescribingPhysician"] = new SelectList(_context.Doctors, "DoctorsSsn", "DoctorsSsn", prescription.PrescribingPhysician);
            return View(prescription);
        }

        // GET: Prescriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }
            ViewData["DrugTradeName"] = new SelectList(_context.Drugs, "TradeName", "TradeName", prescription.DrugTradeName);
            ViewData["PatientSsn"] = new SelectList(_context.Patients, "PatientSsn", "PatientSsn", prescription.PatientSsn);
            ViewData["PrescribingPhysician"] = new SelectList(_context.Doctors, "DoctorsSsn", "DoctorsSsn", prescription.PrescribingPhysician);
            return View(prescription);
        }

        // POST: Prescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PresciptionId,PatientSsn,PrescribingPhysician,PrescriptionDate,Quantity,DrugTradeName")] Prescription prescription)
        {
            if (id != prescription.PresciptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrescriptionExists(prescription.PresciptionId))
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
            ViewData["DrugTradeName"] = new SelectList(_context.Drugs, "TradeName", "TradeName", prescription.DrugTradeName);
            ViewData["PatientSsn"] = new SelectList(_context.Patients, "PatientSsn", "PatientSsn", prescription.PatientSsn);
            ViewData["PrescribingPhysician"] = new SelectList(_context.Doctors, "DoctorsSsn", "DoctorsSsn", prescription.PrescribingPhysician);
            return View(prescription);
        }

        // GET: Prescriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescriptions
                .Include(p => p.DrugTradeNameNavigation)
                .Include(p => p.PatientSsnNavigation)
                .Include(p => p.PrescribingPhysicianNavigation)
                .FirstOrDefaultAsync(m => m.PresciptionId == id);
            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }

        // POST: Prescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription != null)
            {
                _context.Prescriptions.Remove(prescription);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrescriptionExists(int id)
        {
            return _context.Prescriptions.Any(e => e.PresciptionId == id);
        }
    }
}
