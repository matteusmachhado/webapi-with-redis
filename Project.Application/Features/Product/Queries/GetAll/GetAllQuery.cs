using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Product.Queries.GetAll
{
    public sealed record GetAllQuery(int Page, int PageSize) : IRequest<IEnumerable<Response>>;
}
