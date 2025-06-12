
using Application.Interfaces;
using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;


namespace Infrastructure.Repositories
{
    public class SurveyReporitory : GenericRepository<Survey>, ISurveyRepository
    {

        private readonly SurveyContext _context;

        public SurveyReporitory(SurveyContext context) : base(context)
        {
            _context = context;

        }
    public override async Task<Survey> GetByIdAsync(int id)
        {
            return await _context.Survey
                .FirstOrDefaultAsync(p => p.Id == id) ?? throw new KeyNotFoundException($"Player with id {id} was not found.");
        }
    }
}