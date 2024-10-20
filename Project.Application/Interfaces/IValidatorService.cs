using Project.Application.Shared;

namespace Project.Application.Interfaces
{
    public interface IValidatorService
    {
        bool HasErrors();
        void Add(string message);
        IList<ValidatorFailureDto> Validations();
    }
}
