using FluentValidation.Results;
using MediatR;

namespace Project.Application.Features.Product.Commands.Create
{
    public sealed record CreateCommand(string Name, string Description, decimal Price) : IRequest<ValidationResult>;
}
