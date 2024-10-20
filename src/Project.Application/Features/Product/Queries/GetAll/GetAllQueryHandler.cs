using MediatR;
using Project.Domain.Interfaces.Repositories;

namespace Project.Application.Features.Product.Queries.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<Response>>
    {
        private readonly IProductRepository _repository;
        public GetAllQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Response>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync(request.Page, 
                request.PageSize, 
                orderBy: p => p.Price);

            if (products is null) return Enumerable.Empty<Response>();

            var response = products.Select(p => new Response(p.Id, p.Name, p.Description, p.Price));

            return response;
        }
    }
}
