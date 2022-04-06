using FakeItEasy;
using System;
using System.Linq.Expressions;
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

        [Fact]
        public async Task FlagDeletedAsync_MustCallRepository()
        {
            string identifier = "RECORD_123";
            CancellationToken cancellationToken = new();

            await service.FlagAsDeletedAsync(identifier, cancellationToken);

            A.CallTo(() => repository.FlagAsDeletedAsync(identifier, cancellationToken))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => unitOfWork.CommitAsync(cancellationToken))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Todo_Delete_MustInitialize_WithValueFalse()
        {
            Todo todo = new("task", false);

            Assert.False(todo.Deleted);
        }

        [Fact]
        public void Count_MustReturnAmountOfRecords()
        {
            A.CallTo(() => repository.Count()).Returns(5);

            int count = service.Count();

            A.CallTo(() => repository.Count()).MustHaveHappenedOnceExactly();

            Assert.Equal(5, count);
        }


        [Fact]
        public async Task GetManyAsync_MustCallRepository()
        {
            Expression<Func<Todo, bool>> filter = (entity) => entity.Deleted == false;
            int currentPage = 1;
            int pageSize = 10;
            CancellationToken cancellationToken = new();

            await service.GetManyAsync(filter, currentPage, pageSize, cancellationToken);

            A.CallTo(() => repository.GetManyAsync(filter, currentPage, pageSize, cancellationToken))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetOneAsync_MustCallRepository()
        {
            Expression<Func<Todo, bool>> filter = (entity) => entity.Deleted == false;
            CancellationToken cancellationToken = new();

            await service.GetOneAsync(filter, cancellationToken);

            A.CallTo(() => repository.GetOneAsync(filter, cancellationToken))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateAsync_MustReturnTheNewRecord()
        {
            Todo todo = A.Fake<Todo>();
            CancellationToken cancellationToken = new();
            A.CallTo(() => repository.UpdateAsync(todo, cancellationToken))
                .Returns(todo);

            Todo result = await service.UpdateAsync(todo, cancellationToken);

            A.CallTo(() => repository.UpdateAsync(todo, cancellationToken))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => unitOfWork.CommitAsync(cancellationToken))
                .MustHaveHappenedOnceExactly();

            Assert.Equal(todo, result);
        }
    }
}