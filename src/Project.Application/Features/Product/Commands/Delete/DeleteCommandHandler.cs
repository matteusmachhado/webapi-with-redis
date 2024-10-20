using MediatR;
using Project.Domain.Interfaces.Repositories;
using Project.Domain.Interfaces;
using FluentValidation.Results;

namespace Project.Application.Features.Product.Commands.Delete
{
    public class DeleteCommandHandler : CommandHandler, IRequestHandler<DeleteCommand, ValidationResult>
    {
        private readonly IProductRepository _repository;

        public DeleteCommandHandler(IProductRepository repository,
            IUnitOfWork uow) :  base (uow)
        {
            _repository = repository;
        }

        public async Task<ValidationResult> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (product is null) return ValidationResult;

            _repository.Remove(product);

            await _uow.Commit();

            return ValidationResult;
        }
    }
}
