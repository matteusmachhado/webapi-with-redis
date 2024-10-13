using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Product.Commands.Create;
using Project.Application.Features.Product.Queries.GetAll;
using Project.Application.Features.Product.Queries.GetById;
using Project.WebAPI.ViewModels.Product;

namespace Project.WebAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            var product = await _mediator.Send(new GetByIdQuery(id), cancellationToken);

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 10, CancellationToken cancellationToken = default)
        {
            var products = await _mediator.Send(new GetAllQuery(skip, take), cancellationToken);

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProducViewModel request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(request.ToCommand, cancellationToken);

            return Created();
        }
    }
}
