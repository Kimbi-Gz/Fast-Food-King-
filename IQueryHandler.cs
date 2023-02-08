using FastFoodKing.Models;

namespace FastFoodKing
{
    public interface IQueryHandler<M, C> where M : class where C : class
    {
        Task<IEnumerable<M>> GetAll();
        Task<Category> GetOne(C query);
    }
}
