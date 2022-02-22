using AutoMapper;
using TodoApp.API.Registers.MapProfiles;

namespace TodoApp.API.Registers
{
    public static class AutoMapper
    {
        public static void Load(IServiceCollection services)
        {
            services.AddSingleton(register =>
            {
                return new MapperConfiguration(config =>
                {
                    config.AllowNullDestinationValues = true;
                    config.AllowNullCollections = true;

                    config.AddProfile<TodoProfile>();
                }).CreateMapper();
            });
        }
    }
}
