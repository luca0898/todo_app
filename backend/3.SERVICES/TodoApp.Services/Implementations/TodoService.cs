using System.Linq.Expressions;
using TodoApp.Domain.Contracts.Repositories;
using TodoApp.Domain.Contracts.Services;
using TodoApp.Domain.Entities;
using TodoApp.SystemObjects.Contracts;

namespace TodoApp.Services.Implementations
{
    public class TodoService : ITodoService
    {
        private readonly IUnitOfWork _uow;
        private readonly ITodoRepository _repository;

        public TodoService(IUnitOfWork uow, ITodoRepository repository)
        {
            _uow = uow;
            _repository = repository;
        }

        public int Count()
        {
            return _repository.Count();
        }

        public async Task<Todo> CreateAsync(Todo entity, CancellationToken cancellationToken = default)
        {
            Todo createdTodo = await _repository.CreateAsync(entity, cancellationToken);

            await _uow.CommitAsync(cancellationToken);

            return createdTodo;
        }

        public async Task DestroyAsync(string identifier, CancellationToken cancellationToken = default)
        {
            await _repository.DestroyAsync(identifier, cancellationToken);

            await _uow.CommitAsync(cancellationToken);
        }

        public async Task FlagAsDeletedAsync(string identifier, CancellationToken cancellationToken = default)
        {
            await _repository.FlagAsDeletedAsync(identifier, cancellationToken);

            await _uow.CommitAsync(cancellationToken);
        }

        public async Task<IEnumerable<Todo>> GetManyAsync(Expression<Func<Todo, bool>> filter, int currentPage = 1, int pageSize = 20, CancellationToken cancellationToken = default)
        {
            return await _repository.GetManyAsync(filter, currentPage, pageSize, cancellationToken);
        }

        public async Task<Todo?> GetOneAsync(Expression<Func<Todo, bool>> filter, CancellationToken cancellationToken = default)
        {
            return await _repository.GetOneAsync(filter, cancellationToken);
        }

        public async Task<Todo> UpdateAsync(Todo entity, CancellationToken cancellationToken = default)
        {
            await _repository.UpdateAsync(entity, cancellationToken);

            await _uow.CommitAsync(cancellationToken);

            return entity;
        }
    }
}
