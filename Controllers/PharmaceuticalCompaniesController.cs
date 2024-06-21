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
    public class PharmaceuticalCompaniesController : Controller
    {
        private readonly PrescriptionRxDbContext _context;

        public PharmaceuticalCompaniesController(PrescriptionRxDbContext context)
        {
            _context = context;
        }

        // GET: PharmaceuticalCompanies
        public async Task<IActionResult> Index()
        {
            return View(await _context.PharmaceuticalCompanies.ToListAsync());
        }

        // GET: PharmaceuticalCompanies/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmaceuticalCompany = await _context.PharmaceuticalCompanies
                .FirstOrDefaultAsync(m => m.CompanyName == id);
            if (pharmaceuticalCompany == null)
            {
                return NotFound();
            }

            return View(pharmaceuticalCompany);
        }

        // GET: PharmaceuticalCompanies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PharmaceuticalCompanies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SN,CompanyName,PhoneNumber,DrugTradeName")] PharmaceuticalCompany pharmaceuticalCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pharmaceuticalCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pharmaceuticalCompany);
        }

        // GET: PharmaceuticalCompanies/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmaceuticalCompany = await _context.PharmaceuticalCompanies.FindAsync(id);
            if (pharmaceuticalCompany == null)
            {
                return NotFound();
            }
            return View(pharmaceuticalCompany);
        }

        // POST: PharmaceuticalCompanies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SN,CompanyName,PhoneNumber,DrugTradeName")] PharmaceuticalCompany pharmaceuticalCompany)
        {
            if (id != pharmaceuticalCompany.CompanyName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pharmaceuticalCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PharmaceuticalCompanyExists(pharmaceuticalCompany.CompanyName))
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
            return View(pharmaceuticalCompany);
        }

        // GET: PharmaceuticalCompanies/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmaceuticalCompany = await _context.PharmaceuticalCompanies
                .FirstOrDefaultAsync(m => m.CompanyName == id);
            if (pharmaceuticalCompany == null)
            {
                return NotFound();
            }

            return View(pharmaceuticalCompany);
        }

        // POST: PharmaceuticalCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var pharmaceuticalCompany = await _context.PharmaceuticalCompanies.FindAsync(id);
            if (pharmaceuticalCompany != null)
            {
                _context.PharmaceuticalCompanies.Remove(pharmaceuticalCompany);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PharmaceuticalCompanyExists(string id)
        {
            return _context.PharmaceuticalCompanies.Any(e => e.CompanyName == id);
        }
    }
}
