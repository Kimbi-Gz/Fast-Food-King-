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
    public class MenusController : Controller
    {
        private readonly FastFoodKingContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public MenusController(FastFoodKingContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
        
            return View(await _unitOfWork.MenuRepository.GetAllSync());
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var menu = await _unitOfWork.MenuRepository.GetByIdAsync(id); 

            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id");
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Price,CategoryId")] Menu menu)
        {
            _unitOfWork.MenuRepository.Add(menu);
            _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));

        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var menu = await _unitOfWork.MenuRepository.GetByIdAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", menu.CategoryId);
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,CategoryId")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            try
              {
                    _unitOfWork.MenuRepository.Update(menu);
                    _unitOfWork.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
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

        // GET: Menus/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Menu == null)
            {
                return NotFound();
            }

            var menu = await _unitOfWork.MenuRepository.GetByIdAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Menu == null)
            {
                return Problem("Entity set 'FastFoodKingContext.Menu'  is null.");
            }

            _unitOfWork.MenuRepository.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
          return _unitOfWork.MenuRepository.GetByIdAsync(id) != null;
        }
    }
}
