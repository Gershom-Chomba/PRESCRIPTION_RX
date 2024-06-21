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
    public class PharmacyStaffsController : Controller
    {
        private readonly PrescriptionRxDbContext _context;

        public PharmacyStaffsController(PrescriptionRxDbContext context)
        {
            _context = context;
        }

        // GET: PharmacyStaffs
        public async Task<IActionResult> Index()
        {
            return View(await _context.PharmacyStaffs.ToListAsync());
        }

        // GET: PharmacyStaffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacyStaff = await _context.PharmacyStaffs
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (pharmacyStaff == null)
            {
                return NotFound();
            }

            return View(pharmacyStaff);
        }

        // GET: PharmacyStaffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PharmacyStaffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,FirstName,LastName,MiddleName,PlotNumber,Street,City,Gender,StaffPassword,EmailAddress")] PharmacyStaff pharmacyStaff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pharmacyStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pharmacyStaff);
        }

        // GET: PharmacyStaffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacyStaff = await _context.PharmacyStaffs.FindAsync(id);
            if (pharmacyStaff == null)
            {
                return NotFound();
            }
            return View(pharmacyStaff);
        }

        // POST: PharmacyStaffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffId,FirstName,LastName,MiddleName,PlotNumber,Street,City,Gender,StaffPassword,EmailAddress")] PharmacyStaff pharmacyStaff)
        {
            if (id != pharmacyStaff.StaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pharmacyStaff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PharmacyStaffExists(pharmacyStaff.StaffId))
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
            return View(pharmacyStaff);
        }

        // GET: PharmacyStaffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacyStaff = await _context.PharmacyStaffs
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (pharmacyStaff == null)
            {
                return NotFound();
            }

            return View(pharmacyStaff);
        }

        // POST: PharmacyStaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pharmacyStaff = await _context.PharmacyStaffs.FindAsync(id);
            if (pharmacyStaff != null)
            {
                _context.PharmacyStaffs.Remove(pharmacyStaff);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PharmacyStaffExists(int id)
        {
            return _context.PharmacyStaffs.Any(e => e.StaffId == id);
        }
    }
}
