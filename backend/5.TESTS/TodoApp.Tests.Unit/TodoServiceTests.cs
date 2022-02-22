using FakeItEasy;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Domain.Contracts.Repositories;
using TodoApp.Domain.Contracts.Services;
using TodoApp.Domain.Entities;
using TodoApp.Services.Implementations;
using TodoApp.SystemObjects.Contracts;
using Xunit;

namespace TodoApp.Tests.Unit
{
    public class TodoServiceTests
    {
        public readonly ITodoService service;
        public readonly IUnitOfWork unitOfWork;
        public readonly ITodoRepository repository;

        public TodoServiceTests()
        {
            unitOfWork = A.Fake<IUnitOfWork>();
            repository = A.Fake<ITodoRepository>();

            service = new TodoService(unitOfWork, repository);
        }

        [Fact]
        public async Task CreateAsync_MustCommitNewRecord()
        {
            Todo todo = A.Fake<Todo>();
            CancellationToken cancellationToken = new();

            Todo result = await service.CreateAsync(todo, cancellationToken);

            A.CallTo(() => repository.CreateAsync(todo, cancellationToken))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => unitOfWork.CommitAsync(cancellationToken))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task CreateAsync_MustReturnTheNewRecord()
        {
            Todo todo = A.Fake<Todo>();
            CancellationToken cancellationToken = new();
            A.CallTo(() => repository.CreateAsync(todo, cancellationToken))
                .Returns(todo);

            Todo result = await service.CreateAsync(todo, cancellationToken);

            A.CallTo(() => repository.CreateAsync(todo, cancellationToken))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => unitOfWork.CommitAsync(cancellationToken))
                .MustHaveHappenedOnceExactly();

            Assert.Equal(todo, result);
        }

        [Fact]
        public async Task DestroyAsync_MustRemoveTheRecord()
        {
            string identifier = "RECORD_123";
            CancellationToken cancellationToken = new();

            await service.DestroyAsync(identifier, cancellationToken);

            A.CallTo(() => repository.DestroyAsync(identifier, cancellationToken))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => unitOfWork.CommitAsync(cancellationToken))
                .MustHaveHappenedOnceExactly();
        }
    }
}