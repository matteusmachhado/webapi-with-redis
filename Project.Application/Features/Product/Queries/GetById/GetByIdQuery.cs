using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Product.Queries.GetById
{
    public sealed record GetByIdQuery (Guid Id) : IRequest<Response>;
}
