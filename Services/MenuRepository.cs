using FastFoodKing.Data;
using FastFoodKing.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodKing.Services
{
    public class MenuRepository: GenericRepository<Menu>, IMenuRepository
    {
        private readonly FastFoodKingContext _context;
        public MenuRepository(FastFoodKingContext context) : base(context)
        {
            _context= context;
        }

        public override async Task<IEnumerable<Menu>> GetAllSync()
        {
            var fastFoodKingContext = _context.Menu.Include(m => m.Category);
            return await fastFoodKingContext.ToListAsync();
        }

        public  async Task<Menu> GetByIdAsync(int id)
        {
            return await _dbSet.Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

        }

    }
}
