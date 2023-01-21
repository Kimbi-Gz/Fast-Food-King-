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
    public class CartsController : Controller
    {
        private readonly FastFoodKingContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public CartsController(FastFoodKingContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.CartRepository.GetAllSync());
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var cart = await _unitOfWork.CartRepository.GetByIdAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MenuId,UserId,Count")] Cart cart)
        {

            _unitOfWork.CartRepository.Add(cart);
            _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));

        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var cart = await _unitOfWork.CartRepository.GetByIdAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id", cart.MenuId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MenuId,UserId,Count")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }


                try
                {
                    _unitOfWork.CartRepository.Update(cart); 
                    _unitOfWork.Commit();   
          
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
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

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Cart == null)
            {
                return NotFound();
            }

            var cart = await _unitOfWork.CartRepository.GetByIdAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cart == null)
            {
                return Problem("Entity set 'FastFoodKingContext.Cart'  is null.");
            }
            _unitOfWork.CartRepository.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _unitOfWork.CartRepository.GetByIdAsync(id) != null;
        }
    }
}
