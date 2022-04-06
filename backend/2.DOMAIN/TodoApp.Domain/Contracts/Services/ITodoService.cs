using System.Linq.Expressions;
using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Contracts.Services
{
    public interface ITodoService
    {
        Task<Todo?> GetOneAsync(Expression<Func<Todo, bool>> filter, CancellationToken cancellationToken = default);
        Task<IEnumerable<Todo>> GetManyAsync(Expression<Func<Todo, bool>> filter, int currentPage = 1, int pageSize = 20, CancellationToken cancellationToken = default);

        Task<Todo> CreateAsync(Todo entity, CancellationToken cancellationToken = default);
        Task<Todo> UpdateAsync(Todo entity, CancellationToken cancellationToken = default);
        Task FlagAsDeletedAsync(string identifier, CancellationToken cancellationToken = default);
        Task DestroyAsync(string identifier, CancellationToken cancellationToken = default);
        int Count();
    }
}
