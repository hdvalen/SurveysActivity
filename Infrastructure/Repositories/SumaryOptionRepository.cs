
using Application.Interfaces;
using Domain.entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SumaryOptionReporitory : GenericRepository<SummaryOptions>, ISumaryOptionRepository
    {

        private readonly SurveyContext _context;

        public SumaryOptionReporitory(SurveyContext context) : base(context)
        {
            _context = context;

        }
    public override async Task<SummaryOptions> GetByIdAsync(int id)
        {
            return await _context.SummaryOptions
                .FirstOrDefaultAsync(p => p.Id == id) ?? throw new KeyNotFoundException($"Player with id {id} was not found.");
        }
    }
}