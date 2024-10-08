using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Contract_Monthly_Claim_System.Data;
using Contract_Monthly_Claim_System.Models;

namespace Contract_Monthly_Claim_System.Controllers
{
    public class ClaimsModelsController : Controller
    {
        private readonly ContractMonthlyClaimDbContext _context;

        public ClaimsModelsController(ContractMonthlyClaimDbContext context)
        {
            _context = context;
        }

        // GET: ClaimsModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Claims.ToListAsync());
        }

        // GET: ClaimsModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimsModel = await _context.Claims
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claimsModel == null)
            {
                return NotFound();
            }

            return View(claimsModel);
        }

        // GET: ClaimsModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClaimsModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ModuleCode,MonthOfClaim,HoursWorked,HourlyRate,SupportingDocumentPath")] ClaimsModel claimsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(claimsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(claimsModel);
        }

        // GET: ClaimsModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimsModel = await _context.Claims.FindAsync(id);
            if (claimsModel == null)
            {
                return NotFound();
            }
            return View(claimsModel);
        }

        // POST: ClaimsModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ModuleCode,MonthOfClaim,HoursWorked,HourlyRate,SupportingDocumentPath")] ClaimsModel claimsModel)
        {
            if (id != claimsModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claimsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimsModelExists(claimsModel.Id))
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
            return View(claimsModel);
        }

        // GET: ClaimsModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimsModel = await _context.Claims
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claimsModel == null)
            {
                return NotFound();
            }

            return View(claimsModel);
        }

        // POST: ClaimsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var claimsModel = await _context.Claims.FindAsync(id);
            if (claimsModel != null)
            {
                _context.Claims.Remove(claimsModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaimsModelExists(int id)
        {
            return _context.Claims.Any(e => e.Id == id);
        }
    }
}
