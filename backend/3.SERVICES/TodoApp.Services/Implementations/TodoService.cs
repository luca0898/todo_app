using System.Linq.Expressions;
using TodoApp.Domain.Contracts.Repositories;
using TodoApp.Domain.Contracts.Services;
using TodoApp.Domain.Entities;
using TodoApp.Repositories.InMemory;

namespace TodoApp.Services.Implementations
{
    public class TodoService : ITodoService
    {
        private readonly InMemoryDbContext _inMemoryDb;
        private readonly ITodoRepository _repository;

        public TodoService(InMemoryDbContext inMemoryDb, ITodoRepository repository)
        {
            _inMemoryDb = inMemoryDb;
            _repository = repository;
        }

        public async Task<Todo> CreateAsync(Todo entity, CancellationToken cancellationToken = default)
        {
            Todo createdTodo = await _repository.CreateAsync(entity, cancellationToken);

            await _inMemoryDb.SaveChangesAsync(cancellationToken);

            return createdTodo;
        }

        public async Task DestroyAsync(string identifier, CancellationToken cancellationToken = default)
        {
            await _repository.DestroyAsync(identifier, cancellationToken);

            await _inMemoryDb.SaveChangesAsync(cancellationToken);
        }

        public async Task FlagAsDeletedAsync(string identifier, CancellationToken cancellationToken = default)
        {
            await _repository.FlagAsDeletedAsync(identifier, cancellationToken);

            await _inMemoryDb.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Todo>> GetManyAsync(Expression<Func<Todo, bool>> filter, int currentPage = 1, int pageSize = 20, CancellationToken cancellationToken = default)
        {
            return await _repository.GetManyAsync((entity) => !entity.Deleted, currentPage, pageSize, cancellationToken);
        }

        public async Task<Todo?> GetOneAsync(Expression<Func<Todo, bool>> filter, CancellationToken cancellationToken = default)
        {
            return await _repository.GetOneAsync(filter, cancellationToken);
        }

        public async Task<Todo> UpdateAsync(Todo entity, CancellationToken cancellationToken = default)
        {
            await _repository.UpdateAsync(entity, cancellationToken);

            await _inMemoryDb.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
