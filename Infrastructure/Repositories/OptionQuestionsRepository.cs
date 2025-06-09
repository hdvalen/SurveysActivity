
using Application.Interfaces;
using Domain.entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OptionQuestionRepository : GenericRepository<OptionQuestion>, IOptionQuestionsRepository
    {
        private readonly SurveyContext _context;

        public OptionQuestionRepository(SurveyContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<OptionQuestion> GetByIdAsync(int id)
        {
            return await _context.OptionQuestion
                .FirstOrDefaultAsync(p => p.Id == id) ?? throw new KeyNotFoundException($"Player with id {id} was not found.");
        }
    }
}