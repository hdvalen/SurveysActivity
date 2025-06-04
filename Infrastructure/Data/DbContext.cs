
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class SurveyContext : DbContext
    {
        public SurveyContext(DbContextOptions<SurveyContext> options) : base(options)
        {
        }

        public DbSet<Survey> Survey { get; set; }
        public DbSet<SummaryOptions> SummaryOptions { get; set; }
        public DbSet<SubQuestion> SubQuestion { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<OptionsResponse> OptionsResponse { get; set; }
        public DbSet<OptionQuestion> OptionQuestion { get; set; }
        public DbSet<Chapter> Chapter { get; set; }
        public DbSet<CategoryOptions> CategoryOptions { get; set; }
        public DbSet<CategoriesCatalog> CategoriesCatalog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}