
using Application.Interfaces;
using Domain.entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ChapterRepository : GenericRepository<Chapter>, IChapterRepository
    {
        private readonly SurveyContext _context;

        public ChapterRepository(SurveyContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Chapter> GetByIdAsync(int id)
        {
            return await _context.Chapter
                .FirstOrDefaultAsync(p => p.Id == id) ?? throw new KeyNotFoundException($"Player with id {id} was not found.");
        }
    }
}