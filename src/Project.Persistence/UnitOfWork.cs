using Project.Domain.Interfaces;
using Project.Persistence.Contexts;

namespace Project.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectDbContext _context;
        public UnitOfWork(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
