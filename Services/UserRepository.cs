using FastFoodKing.Data;
using FastFoodKing.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodKing.Services
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        private readonly FastFoodKingContext _context;
        public UserRepository(FastFoodKingContext context) : base(context) 
        {
            _context = context;
        }

        public override async Task<IEnumerable<User>> GetAllSync()
        {
            var fastFoodKingContext = _context.User;
            return await fastFoodKingContext.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbSet.Include(c => c.Cart)
                    .FirstOrDefaultAsync(m => m.Id == id);

        }
    }
}
