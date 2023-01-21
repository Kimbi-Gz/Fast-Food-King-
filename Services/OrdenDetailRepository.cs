using FastFoodKing.Data;
using FastFoodKing.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodKing.Services
{
    public class OrdenDetailRepository: GenericRepository<OrdenDetail>, IOrdenDetailRepository
    {
        private readonly FastFoodKingContext _context;

        public OrdenDetailRepository(FastFoodKingContext context) : base(context) 
        {
            _context = context;
        }
        public override async Task<IEnumerable<OrdenDetail>> GetAllSync()
        {
            var fastFoodKingContext = _context.OrdenDetail.Include(o => o.Menu);
            return await fastFoodKingContext.ToListAsync();
        }

        public async Task<OrdenDetail> GetByIdAsync(int id)
        {
            return await _dbSet.Include(o => o.Menu)
                .FirstOrDefaultAsync(o => o.Id == id);

        }
    }
}
