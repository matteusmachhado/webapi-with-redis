using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Product.Commands.Create;
using Project.Application.Features.Product.Queries.GetAll;
using Project.Application.Features.Product.Queries.GetById;
using Project.Application.Interfaces;
using Project.WebAPI.ViewModels.Product;

namespace Project.WebAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : BaseController
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator,
            IValidatorService validator) : base (validator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            return GetResponse(await _mediator.Send(new GetByIdQuery(id), cancellationToken));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            return GetResponse(await _mediator.Send(new GetAllQuery(page, pageSize), cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProducViewModel request, CancellationToken cancellationToken = default)
        {
            return CreatedResponse(await _mediator.Send(request.ToCommand, cancellationToken));
        }
    }
}
