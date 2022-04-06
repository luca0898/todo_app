using TodoApp.SystemObjects.Contracts;

namespace TodoApp.Repositories.InMemory
{
    public class UnitOfWorkInMemory : IUnitOfWork
    {
        private readonly InMemoryDbContext _context;
        private bool _disposed;

        public UnitOfWorkInMemory(InMemoryDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
