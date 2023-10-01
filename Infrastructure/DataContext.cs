namespace Infrastructure
{
    using ApplicationCore.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class DataContext : IdentityDbContext<IdentityUser>
    {
        protected readonly IConfiguration Configuration;

        private DbSet<Quiz> Quizzes { get; set; }
        private DbSet<QuizAttempt> QuizAttempts { get; set; }

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("Default"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Quiz>()
                .HasMany(q => q.Questions)
                .WithOne(q => q.Quiz)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuizQuestion>()
                .HasMany(q => q.Answers)
                .WithOne(q => q.QuizQuestion)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}