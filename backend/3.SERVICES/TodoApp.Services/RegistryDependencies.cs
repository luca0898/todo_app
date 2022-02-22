using Microsoft.Extensions.DependencyInjection;
using TodoApp.Domain.Contracts.Services;
using TodoApp.Services.Implementations;

namespace TodoApp.Services
{
    public static class RegistryDependencies
    {
        public static void LoadServiceDependencies(this IServiceCollection service)
        {
            service.AddTransient<ITodoService, TodoService>();
        }
    }
}