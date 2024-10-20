using MediatR;
using Project.Domain.Interfaces.Repositories;
using Project.Domain.Interfaces;
using FluentValidation.Results;

namespace Project.Application.Features.Product.Commands.Update
{
    public class UpdateCommandHandler : CommandHandler, IRequestHandler<UpdateCommand, ValidationResult>
    {
        private readonly IProductRepository _repository;

        public UpdateCommandHandler(IProductRepository repository,
            IUnitOfWork uow) :  base (uow)
        {
            _repository = repository;
        }

        public async Task<ValidationResult> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (product is null) return ValidationResult;

            product.Update(request.Name, request.Description, request.Price);

            await _repository.Update(product);

            await _uow.Commit();

            return ValidationResult;
        }
    }
}
