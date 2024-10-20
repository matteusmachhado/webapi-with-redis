using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Interfaces;
using Project.Application.Shared;

namespace Project.WebAPI.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly IValidatorService _validator;

        protected BaseController(IValidatorService validator)
        {
            _validator = validator;
        }

        protected ActionResult CreatedResponse(ValidationResult validationResult)
        {
            AddErros(validationResult);

            if (IsValid())
            {
                return Created();
            }

            return Errors;
        }

        protected ActionResult GetResponse(object? response = null)
        {
            if (IsValid())
            {
                return Ok(response);
            }

            return NotFound();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            AddErros(validationResult);

            if (IsValid())
            {
                return Ok();
            }

            return Errors;
        }

        private bool IsValid() => !_validator.HasErrors();

        private ActionResult Errors => BadRequest(new { errors = _validator.Validations().Select(n => n.Message) });

        private void AddErros(ValidationResult validationResult) => validationResult.Errors.ForEach(erro => _validator.Add(erro.ErrorMessage));
    }
}
