using FastFoodKing.Data;
using FastFoodKing.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodKing.Services
{
    public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
    {
        private readonly FastFoodKingContext _context;
        public CategoryRepository(FastFoodKingContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Category>> GetAllSync()
        {
            var fastFoodKingContext = _context.Category;
            return await fastFoodKingContext.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {

            return await _dbSet.Include(c => c.Menu)
             .FirstOrDefaultAsync(m => m.Id == id);

        }
    }
}
