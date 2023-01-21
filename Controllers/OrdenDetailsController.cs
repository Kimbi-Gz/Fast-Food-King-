using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FastFoodKing.Data;
using FastFoodKing.Models;
using FastFoodKing.Configuration;

namespace FastFoodKing.Controllers
{
    public class OrdenDetailsController : Controller
    {
        private readonly FastFoodKingContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public OrdenDetailsController(FastFoodKingContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: OrdenDetails
        public async Task<IActionResult> Index()
        {
            var fastFoodKingContext = _context.OrdenDetail.Include(o => o.Menu);
            return View(await fastFoodKingContext.ToListAsync());
        }

        // GET: OrdenDetails/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var ordenDetail = await _unitOfWork.OrdenDetailRepository.GetByIdAsync(id);
  
            if (ordenDetail == null)
            {
                return NotFound();
            }

            return View(ordenDetail);
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
        public async Task<IActionResult> Create([Bind("Id,MenuId,Count,Description,Total,UserName,Address,phone")] OrdenDetail ordenDetail)
        {
 
                _unitOfWork.OrdenDetailRepository.Add(ordenDetail);
                _unitOfWork.Commit();
                return RedirectToAction(nameof(Index));

        }

        // GET: OrdenDetails/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var ordenDetail = await _unitOfWork.OrdenDetailRepository.GetByIdAsync(id);
            if (ordenDetail == null)
            {
                return NotFound();
            }
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id", ordenDetail.MenuId);
            return View(ordenDetail);
        }

        // POST: OrdenDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MenuId,Count,Description,Total,UserName,Address,phone")] OrdenDetail ordenDetail)
        {
            if (id != ordenDetail.Id)
            {
                return NotFound();
            }

                try
                {
                  _unitOfWork.OrdenDetailRepository.Update(ordenDetail);
                  _unitOfWork.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenDetailExists(ordenDetail.Id))
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

        // GET: OrdenDetails/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.OrdenDetail == null)
            {
                return NotFound();
            }

            var ordenDetail = await _unitOfWork.OrdenDetailRepository.GetByIdAsync(id);
            if (ordenDetail == null)
            {
                return NotFound();
            }

            return View(ordenDetail);
        }

        // POST: OrdenDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrdenDetail == null)
            {
                return Problem("Entity set 'FastFoodKingContext.OrdenDetail'  is null.");
            }
         
            _unitOfWork.OrdenDetailRepository.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenDetailExists(int id)
        {
          return _unitOfWork.OrdenDetailRepository.GetByIdAsync(id) != null;
        }
    }
}
