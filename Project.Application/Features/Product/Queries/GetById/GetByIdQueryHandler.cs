using FluentValidation;
using MediatR;
using Project.Application.Interfaces;
using Project.Domain.Interfaces.Repositories;

namespace Project.Application.Features.Product.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Response?>
    {
        private readonly IProductRepository _repository;
        private readonly IValidatorService _validator;

        public GetByIdQueryHandler(IProductRepository repository,
            IValidatorService validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<Response?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);

            if (product is null)
            {
                _validator.Add($"Produto: {request.Id} não encontrado.");

                return null;
            }

            var response = new Response(product.Id, product.Name, product.Description, product.Price);

            return response;
        }
    }
}
