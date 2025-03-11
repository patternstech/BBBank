using AutoMapper;
using BBBankAPI;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class AutoMapperExtensions
{
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfiles()); // ✅ Add mapping profile
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}
