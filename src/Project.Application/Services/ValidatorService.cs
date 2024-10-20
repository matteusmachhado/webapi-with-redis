using Project.Application.Interfaces;
using Project.Application.Shared;

namespace Project.Application.Services
{
    public class ValidatorService : IValidatorService
    {
        private List<ValidatorFailureDto> _validations;

        public ValidatorService()
        {
            _validations = new List<ValidatorFailureDto>();
        }

        public void Add(string message)
        {
            _validations.Add(new ValidatorFailureDto(message));
        }

        public bool HasErrors()
        {
            return _validations.Any();
        }

        public IList<ValidatorFailureDto> Validations()
        {
            return _validations;
        }
    }
}
