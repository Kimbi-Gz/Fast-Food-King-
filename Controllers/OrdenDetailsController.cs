using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FastFoodKing.Data;
using FastFoodKing.Models;

namespace FastFoodKing.Controllers
{
    public class OrdenDetailsController : Controller
    {
        private readonly FastFoodKingContext _context;

        public OrdenDetailsController(FastFoodKingContext context)
        {
            _context = context;
        }

        // GET: OrdenDetails
        public async Task<IActionResult> Index()
        {
            var fastFoodKingContext = _context.OrdenDetails.Include(o => o.Menu);
            return View(await fastFoodKingContext.ToListAsync());
        }

        // GET: OrdenDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrdenDetails == null)
            {
                return NotFound();
            }

            var ordenDetails = await _context.OrdenDetails
                .Include(o => o.Menu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordenDetails == null)
            {
                return NotFound();
            }

            return View(ordenDetails);
        }

        // GET: OrdenDetails/Create
        public IActionResult Create()
        {
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id");
            return View();
        }

        // POST: OrdenDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MenuId,Count,Description,Total,UserName,Address,phone")] OrdenDetails ordenDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordenDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id", ordenDetails.MenuId);
            return View(ordenDetails);
        }

        // GET: OrdenDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrdenDetails == null)
            {
                return NotFound();
            }

            var ordenDetails = await _context.OrdenDetails.FindAsync(id);
            if (ordenDetails == null)
            {
                return NotFound();
            }
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id", ordenDetails.MenuId);
            return View(ordenDetails);
        }

        // POST: OrdenDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MenuId,Count,Description,Total,UserName,Address,phone")] OrdenDetails ordenDetails)
        {
            if (id != ordenDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordenDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenDetailsExists(ordenDetails.Id))
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
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id", ordenDetails.MenuId);
            return View(ordenDetails);
        }

        // GET: OrdenDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrdenDetails == null)
            {
                return NotFound();
            }

            var ordenDetails = await _context.OrdenDetails
                .Include(o => o.Menu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordenDetails == null)
            {
                return NotFound();
            }

            return View(ordenDetails);
        }

        // POST: OrdenDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrdenDetails == null)
            {
                return Problem("Entity set 'FastFoodKingContext.OrdenDetails'  is null.");
            }
            var ordenDetails = await _context.OrdenDetails.FindAsync(id);
            if (ordenDetails != null)
            {
                _context.OrdenDetails.Remove(ordenDetails);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenDetailsExists(int id)
        {
          return _context.OrdenDetails.Any(e => e.Id == id);
        }
    }
}
