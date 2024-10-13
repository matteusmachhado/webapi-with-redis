using MediatR;
using Project.Domain.Interfaces;
using Project.Domain.Interfaces.Repositories;

namespace Project.Application.Features.Product.Commands.Create
{

    public class CreateCommandHandler : IRequestHandler<CreateCommand, Unit>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _uow;
        public CreateCommandHandler(IProductRepository repository,
            IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        public async Task<Unit> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var product = new Domain.Entities.Product(request.Name, request.Description, request.Price);

            await _repository.Add(product);

            await _uow.Commit();

            return Unit.Value;
        }
    }
}
