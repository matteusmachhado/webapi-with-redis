using FluentValidation.Results;
using MediatR;

namespace Project.Application.Features.Product.Commands.Delete
{
    public sealed record DeleteCommand(Guid Id) : IRequest<ValidationResult>;
}
