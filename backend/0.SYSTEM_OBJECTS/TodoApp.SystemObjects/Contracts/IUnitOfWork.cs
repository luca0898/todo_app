namespace TodoApp.SystemObjects.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
