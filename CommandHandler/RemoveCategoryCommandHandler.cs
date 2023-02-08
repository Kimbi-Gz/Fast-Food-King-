using FastFoodKing.Commands;
using FastFoodKing.Configuration;

namespace FastFoodKing.CommandHandler
{
    public class RemoveCategoryCommandHandler : ICommandHandler<RemoveByIdCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RemoveCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public CommandResult Execute(RemoveByIdCommand command)
        {
            _unitOfWork.Category.Delete(command.Id);
            _unitOfWork.Commit();
            return new CommandResult { Status = true, Message = "Permission added succesfully" };
        }
    }
}
