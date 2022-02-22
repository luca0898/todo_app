using Microsoft.EntityFrameworkCore;
using TodoApp.Repositories.InMemory;

namespace TodoApp.API.Registers
{
    public static class Repositories
    {
        public static void Load(IServiceCollection services)
        {
            services.AddDbContext<InMemoryDbContext>(options =>
            {
                options
                    .UseInMemoryDatabase("TodoAppInMemoryDb")
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.LoadInMemoryRepositories();
        }
    }
}
