using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Product.Commands.Create
{
    public sealed record CreateCommand(string Name, string Description, decimal Price) : IRequest<Unit>;
}
