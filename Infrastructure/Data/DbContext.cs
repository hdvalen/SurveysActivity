
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Domain.entities;

namespace Infrastructure.Data
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
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberRols> MemberRols { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserMember> UserMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}