namespace TodoApp.API.Registers
{
    public static class Mvc
    {
        public static void Load(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc(options => options.SuppressAsyncSuffixInActionNames = false);
            services.AddEndpointsApiExplorer();
        }
    }
}
