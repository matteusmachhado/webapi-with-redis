using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Product.Queries.GetById
{
    public sealed record Response(Guid Id, string Name, string Description, decimal Price);
}
