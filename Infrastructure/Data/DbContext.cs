
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class SurveyContext : DbContext
    {
        public SurveyContext(DbContextOptions<SurveyContext> options) : base(options)
        {
        }
    }
}