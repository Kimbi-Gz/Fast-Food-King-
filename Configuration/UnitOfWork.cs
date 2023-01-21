using FastFoodKing.Data;
using FastFoodKing.Services;

namespace FastFoodKing.Configuration
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly FastFoodKingContext _context; 

        public IMenuRepository MenuRepository { get; private set; }

        public ICategoryRepository CategoryRepository { get; private set; }

        public IOrdenDetailRepository OrdenDetailRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public ICartRepository CartRepository { get; private set; }

        public UnitOfWork(FastFoodKingContext context)
        {
            _context= context;
            MenuRepository = new MenuRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            OrdenDetailRepository= new OrdenDetailRepository(_context);
            UserRepository = new UserRepository(_context);
            CartRepository = new CartRepository(_context);
     

        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
