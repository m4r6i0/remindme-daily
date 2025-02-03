using Microsoft.EntityFrameworkCore;
using RemindMeDaily.Domain.Entities;

namespace RemindMeDaily.API.Data
{
    public class AppDbContext : DbContext
    {
        // Definir DbSets para suas entidades
        public DbSet<Reminder> Reminders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public AppDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

                var databaseFolder = configuration.GetSection("DatabaseSettings:DatabaseFolder").Value;
                var databaseName = configuration.GetSection("DatabaseSettings:DatabaseName").Value;

                var databaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, databaseFolder);

                if (!Directory.Exists(databaseDirectory))
                {
                    Directory.CreateDirectory(databaseDirectory);
                }

                // Define o caminho do banco de dados na pasta Data
                var databasePath = Path.Combine(databaseDirectory, databaseName);
                optionsBuilder.UseSqlite($"Data Source={databasePath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reminder>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(400);
                entity.Property(e => e.ReminderDate).IsRequired();
            });
        }
    }
}
