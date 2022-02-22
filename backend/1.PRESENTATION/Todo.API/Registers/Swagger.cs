namespace TodoApp.API.Registers
{
    public static class Swagger
    {
        public static void Load (IServiceCollection services)
        {
            services.AddSwaggerGen();
        }
    }
}
