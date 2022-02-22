namespace TodoApp.API.Registers
{
    public static class Cors
    {
        public static void Load(IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy((builder) =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }
    }
}
