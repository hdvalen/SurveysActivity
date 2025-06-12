
using Application.Interfaces;
using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;


namespace Infrastructure.Repositories
{
    public class OptionsResponseRepository : GenericRepository<OptionsResponse>, IOptionsResponseRepository
    {
        private readonly SurveyContext _context;

        public OptionsResponseRepository(SurveyContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<OptionsResponse> GetByIdAsync(int id)
        {
            return await _context.OptionsResponse
                .FirstOrDefaultAsync(p => p.Id == id) ?? throw new KeyNotFoundException($"Player with id {id} was not found.");
        }
    }
}