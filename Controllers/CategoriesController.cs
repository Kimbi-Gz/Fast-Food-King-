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
using FastFoodKing.Commands;
using FastFoodKing.DTOs;
using FastFoodKing.QueryHandler;

namespace FastFoodKing.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICommandHandler<CategoryDTO> _categoryCommandHandler;
        private readonly ICommandHandler<RemoveByIdCommand> _removeCommandHandler;
        private readonly IQueryHandler<Category, QueryByIdCommand> _categoryQueryHandler;


        public CategoriesController(
           ICommandHandler<CategoryDTO> categoryCommandHandler,
           ICommandHandler<RemoveByIdCommand> removeCommandHandler,
           IQueryHandler<Category, QueryByIdCommand> categoryQueryHandler
           )
        {
            _categoryCommandHandler = categoryCommandHandler;
            _removeCommandHandler = removeCommandHandler;
            _categoryQueryHandler = categoryQueryHandler;

        }
        // GET: Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            //_kafkaProducerHandler.WriteMessage("GET");
            var categories = await _categoryQueryHandler.GetAll();
            return Ok(categories);
        }


        // GET: Categories/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            //_kafkaProducerHandler.WriteMessage("GET");
            var category = await _categoryQueryHandler.GetOne(new QueryByIdCommand()
            {
                Id = id
            });

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: Categories/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryDTO category)
        {
            // _kafkaProducerHandler.WriteMessage("PUT");
            if (id != category.Id)
            {
                return BadRequest();
            }

            _categoryCommandHandler.Execute(category);
            return NoContent();
        }

        // POST: Categories       
        [HttpPost]
        public IActionResult PostCategory(CategoryDTO category)
        {
            //_kafkaProducerHandler.WriteMessage("POST");
            _categoryCommandHandler.Execute(category);
            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            //_kafkaProducerHandler.WriteMessage("DELETE");
            _removeCommandHandler.Execute(new RemoveByIdCommand()
            {
                Id = id
            });
            return NoContent();
        }
    }

}
