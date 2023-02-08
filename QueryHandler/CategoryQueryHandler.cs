using FastFoodKing.Commands;
using FastFoodKing.Configuration;
using FastFoodKing.Models;

namespace FastFoodKing.QueryHandler
{
    public class CategoryQueryHandler : IQueryHandler<Category, QueryByIdCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _unitOfWork.Category.All(); 
        }

        public async Task<Category> GetOne(QueryByIdCommand query)
        {
            return await _unitOfWork.Category.GetById(query.Id);
        }
    }
}
