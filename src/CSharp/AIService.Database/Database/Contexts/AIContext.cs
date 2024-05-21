using AIService.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AIService.Database.Contexts;
public class AIContext : DbContext
{
    readonly IConfiguration _configuration;
    public AIContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<BotEntity> Bots { get; set; }
    public DbSet<BotDataEntity> BotDatas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration["ConnectionString"]);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BotEntity>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(indexExpression: x => new { x.TenantId, x.Name })
                .IsUnique();

            e.HasData(new BotEntity()
            {
                Id = 1,
                Model = "gpt-4",
                Name = "Main",
                TenantId = 1,
            });
        });

        modelBuilder.Entity<BotDataEntity>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(indexExpression: x => x.Role);
            e.HasOne(x => x.Bot)
                .WithMany(x => x.BotDatas);
            e.HasData(new BotDataEntity()
            {
                Id = 1,
                BotId = 1,
                Name = "Initial",
                Content = "My name is Ali",
                Role = "system"
            });
        });
    }
}