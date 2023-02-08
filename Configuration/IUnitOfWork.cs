using FastFoodKing.Services;

namespace FastFoodKing.Configuration
{
    public interface IUnitOfWork
    {
        IMenuRepository MenuRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IOrdenDetailRepository OrdenDetailRepository { get; }
        IUserRepository UserRepository { get; }
        ICartRepository CartRepository { get; }
        object Category { get; }

        void Commit();
        void Dispose();
    }
}
