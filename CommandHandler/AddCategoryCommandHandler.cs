using FastFoodKing.Configuration;
using FastFoodKing.DTOs;
using FastFoodKing.Models;

namespace FastFoodKing.CommandHandler
{
    public class AddCategoryCommandHandler : ICommandHandler<CategoryDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public CommandResult Execute(CategoryDTO category)
        {
            var newCategory = new Category()
            {
                Id = category.Id,
                Title = category.Title,
 
            };
            _unitOfWork.Category.Add(newCategory);
            _unitOfWork.Commit();

            return new CommandResult { Status = true, Message = "Permission added succesfully" };
        }

    }
}
