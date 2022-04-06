using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Domain.Contracts.Repositories;
using TodoApp.Domain.Contracts.Services;
using TodoApp.Domain.Entities;
using TodoApp.Repositories.InMemory;
using TodoApp.Repositories.InMemory.Implementations;
using TodoApp.Services.Implementations;
using TodoApp.SystemObjects.Contracts;
using Xunit;

namespace TodoApp.Tests.Unit
{
    public class TodoServiceTests
    {
        public ITodoService service;
        public IUnitOfWork unitOfWork;
        public DbContextOptions<InMemoryDbContext> options = new DbContextOptionsBuilder<InMemoryDbContext>()
            .UseInMemoryDatabase("TodoAppInMemoryDbContext")
            .Options;
        public InMemoryDbContext dbContext;
        public ITodoRepository repository;

        public TodoServiceTests()
        {
            dbContext = new(options);
            unitOfWork = new UnitOfWorkInMemory(dbContext);
            repository = new TodoRepository(dbContext);

            service = new TodoService(unitOfWork, repository);
        }

        [Fact]
        public async Task MustBeAbleToCountRecords()
        {
            CancellationToken cancellationToken = new();

            Todo first = new("first task");
            await service.CreateAsync(first, cancellationToken);

            Todo second = new("second task");
            await service.CreateAsync(second, cancellationToken);

            int totalRecords = service.Count();

            Assert.Equal(2, totalRecords);

            // remove after
            dbContext.TodoList.Remove(first);
            dbContext.TodoList.Remove(second);
            dbContext.SaveChanges();
        }

        [Fact]
        public async Task MustBeAbleToQueryNewlyRecords()
        {
            Todo todo = new("new task");
            CancellationToken cancellationToken = new();

            Todo createdTodo = await service.CreateAsync(todo, cancellationToken);

            bool dbContextContainCreatedTodo = dbContext.TodoList
                 .AsQueryable()
                 .Where(e => e.Id == createdTodo.Id)
                 .Contains(createdTodo);

            Assert.True(dbContextContainCreatedTodo);

            // remove after
            dbContext.TodoList.Remove(createdTodo);
            dbContext.SaveChanges();
        }

        [Fact]
        public async Task MustBeToQueryRecordsAccordingToFilterParameter()
        {
            CancellationToken cancellationToken = new();

            foreach (int value in Enumerable.Range(1, 20))
            {
                Todo todo = new($"{value}st task");
                todo.Finished = value % 2 == 0; // is even, so let's define as finished
                await service.CreateAsync(todo, cancellationToken);
            }

            var results = await service.GetManyAsync(e => e.Finished, 1, 20, cancellationToken);

            Assert.Equal(10, results.Count());

            // remove after
            dbContext.TodoList.RemoveRange(dbContext.TodoList.ToList());
            dbContext.SaveChanges();
        }

        [Fact]
        public async Task MustBeAbleToUpdateTheRecord()
        {
            CancellationToken cancellationToken = new();

            Todo todo = new($"new task", false); // ensure the task is not finished
            await service.CreateAsync(todo, cancellationToken);

            todo.Finished = true;
            Todo todoAfterUpdate = await service.UpdateAsync(todo, cancellationToken);

            Assert.True(todoAfterUpdate.Finished, "Returned data has not been updated");

            // remove after
            dbContext.TodoList.Remove(todoAfterUpdate);
            dbContext.SaveChanges();
        }

        [Fact]
        public async Task MustNotBeAbleToQueryDestroyedRecords()
        {
            Todo todo = new("new task");
            CancellationToken cancellationToken = new();

            Todo createdTodo = await service.CreateAsync(todo, cancellationToken);

            await service.DestroyAsync(createdTodo.Id, cancellationToken);

            Todo createdEntityExist = await service.GetOneAsync(e => e.Id.Equals(createdTodo.Id), cancellationToken);

            Assert.True(createdEntityExist == null, "The created entity should be destroyed");
        }

        [Fact]
        public async Task MustNotBeAbleToQueryDeletedRecords()
        {
            Todo todo = new("new task");
            CancellationToken cancellationToken = new();

            Todo createdTodo = await service.CreateAsync(todo, cancellationToken);

            await service.FlagAsDeletedAsync(createdTodo.Id, cancellationToken);

            Todo createdEntityExist = await service.GetOneAsync(e => e.Id.Equals(createdTodo.Id), cancellationToken);

            Assert.True(createdEntityExist.Deleted, "The created entity should be flagged as deleted");

            // remove after
            dbContext.TodoList.Remove(createdTodo);
            dbContext.SaveChanges();
        }
    }
}