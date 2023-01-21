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
    public class CategoriesController : Controller
    {
        private readonly FastFoodKingContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(FastFoodKingContext context, UnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
              return View(await _unitOfWork.CategoryRepository.GetAllSync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] Category category)
        {
            _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));

        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id); 
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

                try
                {
                     _unitOfWork.CategoryRepository.Update(category);
                     _unitOfWork.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
              
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Category == null)
            {
                return Problem("Entity set 'FastFoodKingContext.Category'  is null.");
            }
            _unitOfWork.CategoryRepository.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return _unitOfWork.CategoryRepository.GetByIdAsync(id) != null;
        }
    }
}
