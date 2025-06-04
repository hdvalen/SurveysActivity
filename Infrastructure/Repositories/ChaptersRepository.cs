
using Application.Interfaces;
using Domain.entities;

namespace Infrastructure.Repositories
{
    public class ChapterRepository : GenericRepository<Chapter>, IChapterRepository
    {
        private readonly SurveyContext _context;

        public ChapterRepository(SurveyContext context) : base(context)
        {
            _context = context;
        }
    }
}