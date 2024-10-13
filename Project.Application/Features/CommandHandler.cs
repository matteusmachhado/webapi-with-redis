using Project.Domain.Interfaces;

namespace Project.Application.Features
{
    public abstract class CommandHandler
    {
        private readonly IUnitOfWork _uow;

        public CommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected async Task Commit()
        {
            await _uow.Commit();
        }
    }
}
