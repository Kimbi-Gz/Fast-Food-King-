using FastFoodKing.Data;
using FastFoodKing.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodKing.Services
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly FastFoodKingContext _context;
        public CartRepository(FastFoodKingContext context) : base(context) 
        {
            _context = context;
        }

        public override async Task<IEnumerable<Cart>> GetAllSync()
        {
            var fastFoodKingContext = _context.Cart.Include(c => c.Menu);
            return await fastFoodKingContext.ToListAsync();
        }

        public async Task<Cart> GetByIdAsync(int id)
        {
            return await _dbSet.Include(c => c.Menu)
                .FirstOrDefaultAsync(m => m.Id == id);

        }
    }
}
