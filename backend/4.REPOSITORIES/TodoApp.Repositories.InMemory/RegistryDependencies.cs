using Microsoft.Extensions.DependencyInjection;
using TodoApp.Domain.Contracts.Repositories;
using TodoApp.Repositories.InMemory.Implementations;
using TodoApp.SystemObjects.Contracts;

namespace TodoApp.Repositories.InMemory
{
    public static class RegistryDependencies
    {
        public static void LoadInMemoryRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWorkInMemory>();
            services.AddTransient<ITodoRepository, TodoRepository>();
        }
    }
}
