namespace TodoApp.API.Registers
{
    public static class LoadContainer
    {
        public static void RegistryServices(this IServiceCollection services)
        {
            AutoMapper.Load(services);
            Repositories.Load(services);
            Services.Load(services);
            Cors.Load(services);
            Mvc.Load(services);
            Swagger.Load(services);
        }
    }
}
