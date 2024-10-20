using FluentValidation.Results;
using MediatR;
using Project.Domain.Interfaces;
using Project.Domain.Interfaces.Repositories;
using System.Drawing;
using System.Globalization;

namespace Project.Application.Features.Product.Commands.Create
{

    public class CreateCommandHandler : CommandHandler, IRequestHandler<CreateCommand, ValidationResult>
    {
        private readonly IProductRepository _repository;

        public CreateCommandHandler(IProductRepository repository,
            IUnitOfWork uow): base (uow) 
        {
            _repository = repository;
        }

        public async Task<ValidationResult> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            if (!IsValid(request)) return ValidationResult;

            var product = new Domain.Entities.Product(request.Name, request.Description, request.Price);

            await _repository.Add(product);

            await _uow.Commit();

            return ValidationResult;
        }

        private bool IsValid(CreateCommand request)
        {
            /* 
             * Validações adicionais
             * Exemplo: consulta uma tabela de processo de outro serviço.
             */
            var minTableValue = 0.99m;
            if (request.Price < minTableValue)
            {
                ValidationResult.Errors.Add(new ValidationFailure(nameof(request.Price), 
                    $"O preço do produto deve ser maior que o valor informado na tebala XPTO: {minTableValue.ToString("C", new CultureInfo("pt-BR"))}"));
            }

            return !ValidationResult.Errors.Any();
        }
    }
}
