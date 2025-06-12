using Application.Interfaces;
using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;


namespace Infrastructure.Repositories
{
    public class QuestionsRepository : GenericRepository<Question>, IQuestionsRepository
    {
        private readonly SurveyContext _context;

        public QuestionsRepository(SurveyContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<Question> GetByIdAsync(int id)
        {
            return await _context.Question
                .FirstOrDefaultAsync(p => p.Id == id) ?? throw new KeyNotFoundException($"Player with id {id} was not found.");
        }
    }
}