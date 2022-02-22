using TodoApp.Services;

namespace TodoApp.API.Registers
{
    public static class Services
    {
        public static void Load(IServiceCollection services)
        {
            services.LoadServiceDependencies();
        }
    }
}
