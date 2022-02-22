using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using TodoApp.Domain.Contracts.Repositories;
using TodoApp.Domain.Entities;

namespace TodoApp.Repositories.InMemory.Implementations
{
    public class TodoRepository : ITodoRepository
    {
        private readonly InMemoryDbContext _context;
        private readonly DbSet<Todo> _db;

        public TodoRepository(InMemoryDbContext context)
        {
            _context = context;
            _db = context.Set<Todo>();
        }

        public int Count()
        {
            return _db.AsQueryable().Count();
        }

        public async Task<int> CountAsync(Expression<Func<Todo, bool>> filter, CancellationToken cancellationToken = default)
        {
            return await _db
                .AsQueryable()
                .Where(filter)
                .CountAsync(cancellationToken);
        }

        public async Task<Todo> CreateAsync(Todo entity, CancellationToken cancellationToken = default)
        {
            EntityEntry<Todo>? createdEntity = await _db.AddAsync(entity, cancellationToken);

            return createdEntity.Entity;
        }

        public async Task DestroyAsync(string identifier, CancellationToken cancellationToken = default)
        {
            Todo? existingTodo = await _db
                .AsQueryable()
                .FirstOrDefaultAsync(todo => todo.Id == identifier, cancellationToken);

            if (existingTodo == null)
            {
                throw new Exception(string.Format("Todo with id {0} not found", identifier));
            }

            _db.Remove(existingTodo);
        }

        public async Task FlagAsDeletedAsync(string identifier, CancellationToken cancellationToken = default)
        {
            Todo? existingTodo = await _db
                .AsQueryable()
                .FirstOrDefaultAsync(todo => todo.Id == identifier, cancellationToken);

            if (existingTodo == null)
            {
                throw new Exception(string.Format("Todo with id {0} not found", identifier));
            }

            Todo modifiedTodo = existingTodo;
            modifiedTodo.Deleted = true;

            _context.Entry(modifiedTodo).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Todo>> GetManyAsync(Expression<Func<Todo, bool>> filter, int currentPage = 1, int pageSize = 20, CancellationToken cancellationToken = default)
        {
            return await _db
                .AsQueryable()
                .Where(filter)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<Todo?> GetOneAsync(Expression<Func<Todo, bool>> filter, CancellationToken cancellationToken = default)
        {
            return await _db
                .AsQueryable()
                .Where(filter)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Todo> UpdateAsync(Todo entity, CancellationToken cancellationToken = default)
        {
            Todo? existingTodo = await _db
                .AsQueryable()
                .FirstOrDefaultAsync(todo => todo.Id == entity.Id, cancellationToken);

            if (existingTodo == null)
            {
                throw new Exception(string.Format("Todo with id {0} not found", entity.Id));
            }

            _context.Entry(entity).State = EntityState.Modified;

            return entity;
        }
    }
}
