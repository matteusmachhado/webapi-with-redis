using Project.Domain.Interfaces;

namespace Project.Application.Services
{
    public abstract class BaseService
    {
        private readonly IUnitOfWork _uow;

        protected BaseService(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}
