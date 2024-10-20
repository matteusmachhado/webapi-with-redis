using FluentValidation.Results;
using Project.Domain.Interfaces;

namespace Project.Application.Features
{
    public abstract class CommandHandler
    {
        protected readonly IUnitOfWork _uow;
        protected ValidationResult ValidationResult;

        public CommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
            ValidationResult ??= new ValidationResult();
        }

        protected async Task Commit()
        {
            await _uow.Commit();
        }
    }
}
