using AICore.WebApi;
using AIService.Bots;
using AIService.Database.Contexts;
using AIService.Logics.Interfaces;
using AIService.Mappers;
using AutoMapper;
using EasyMicroservices.Database.EntityFrameworkCore.Providers;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.Mapper.AutoMapper.Providers;
using EasyMicroservices.Mapper.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AIService.WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        await ApplicationExecutor.RunApplication(args, (builder) =>
        {
            // Add services to the container.
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            builder.Services.AddScoped<IDatabase>(services =>
            {
                var config = services.GetRequiredService<IConfiguration>();
                return new EntityFrameworkCoreDatabaseProvider(new AIContext(config));
            });
            builder.Services.AddScoped<IMapperProvider>(x => new AutoMapperProvider(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            })));
            builder.Services.AddScoped<IBotService, ChatGpt>();
        }, async app =>
        {
            using (var scope = app.Services.CreateScope())
            {
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                using var context = new AIContext(config);
                await context.Database.MigrateAsync();
            }
        });
    }
}
