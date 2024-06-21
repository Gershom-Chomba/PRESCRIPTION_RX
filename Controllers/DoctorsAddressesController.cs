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
    public class DoctorsAddressesController : Controller
    {
        private readonly PrescriptionRxDbContext _context;

        public DoctorsAddressesController(PrescriptionRxDbContext context)
        {
            _context = context;
        }

        // GET: DoctorsAddresses
        public async Task<IActionResult> Index()
        {
            var prescriptionRxDbContext = _context.DoctorsAddresses.Include(d => d.DoctorSsnNavigation);
            return View(await prescriptionRxDbContext.ToListAsync());
        }

        // GET: DoctorsAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorsAddress = await _context.DoctorsAddresses
                .Include(d => d.DoctorSsnNavigation)
                .FirstOrDefaultAsync(m => m.SN == id);
            if (doctorsAddress == null)
            {
                return NotFound();
            }

            return View(doctorsAddress);
        }

        // GET: DoctorsAddresses/Create
        public IActionResult Create()
        {
            ViewData["DoctorSsn"] = new SelectList(_context.Doctors, "DoctorsSsn", "DoctorsSsn");
            return View();
        }

        // POST: DoctorsAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SN,DoctorSsn,PlotNumber,Street,City")] DoctorsAddress doctorsAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctorsAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorSsn"] = new SelectList(_context.Doctors, "DoctorsSsn", "DoctorsSsn", doctorsAddress.DoctorSsn);
            return View(doctorsAddress);
        }

        // GET: DoctorsAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorsAddress = await _context.DoctorsAddresses.FindAsync(id);
            if (doctorsAddress == null)
            {
                return NotFound();
            }
            ViewData["DoctorSsn"] = new SelectList(_context.Doctors, "DoctorsSsn", "DoctorsSsn", doctorsAddress.DoctorSsn);
            return View(doctorsAddress);
        }

        // POST: DoctorsAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SN,DoctorSsn,PlotNumber,Street,City")] DoctorsAddress doctorsAddress)
        {
            if (id != doctorsAddress.SN)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctorsAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorsAddressExists(doctorsAddress.SN))
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
            ViewData["DoctorSsn"] = new SelectList(_context.Doctors, "DoctorsSsn", "DoctorsSsn", doctorsAddress.DoctorSsn);
            return View(doctorsAddress);
        }

        // GET: DoctorsAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorsAddress = await _context.DoctorsAddresses
                .Include(d => d.DoctorSsnNavigation)
                .FirstOrDefaultAsync(m => m.SN == id);
            if (doctorsAddress == null)
            {
                return NotFound();
            }

            return View(doctorsAddress);
        }

        // POST: DoctorsAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctorsAddress = await _context.DoctorsAddresses.FindAsync(id);
            if (doctorsAddress != null)
            {
                _context.DoctorsAddresses.Remove(doctorsAddress);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorsAddressExists(int id)
        {
            return _context.DoctorsAddresses.Any(e => e.SN == id);
        }
    }
}
