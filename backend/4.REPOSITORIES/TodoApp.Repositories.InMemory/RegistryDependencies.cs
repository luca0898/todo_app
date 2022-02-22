using Microsoft.Extensions.DependencyInjection;
using TodoApp.Domain.Contracts.Repositories;
using TodoApp.Repositories.InMemory.Implementations;

namespace TodoApp.Repositories.InMemory
{
    public static class RegistryDependencies
    {
        public static void LoadInMemoryRepositories(this IServiceCollection services)
        {
            services.AddTransient<ITodoRepository, TodoRepository>();
        }
    }
}
