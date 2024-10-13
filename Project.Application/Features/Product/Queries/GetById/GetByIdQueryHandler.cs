using MediatR;
using Project.Domain.Interfaces.Repositories;

namespace Project.Application.Features.Product.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Response>
    {
        private readonly IProductRepository _repository;
        public GetByIdQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);

            if (product is null)
            {
                // todo: Not found
                return null;
            }

            var response = new Response(product.Id, product.Name, product.Description, product.Price);

            return response;
        }
    }
}
