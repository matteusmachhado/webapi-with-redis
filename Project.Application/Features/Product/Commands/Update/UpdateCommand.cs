using FluentValidation.Results;
using MediatR;

namespace Project.Application.Features.Product.Commands.Update
{
    public sealed record UpdateCommand(Guid Id, string Name, string Description, decimal Price) : IRequest<ValidationResult>;
}
