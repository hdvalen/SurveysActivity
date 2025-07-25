
using Application.Interfaces;
using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;


namespace Infrastructure.Repositories
{
    public class CategoryOptionsRepository : GenericRepository<CategoryOptions>, ICategoryOptionsRepository
    {
        private readonly SurveyContext _context;

        public CategoryOptionsRepository(SurveyContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<CategoryOptions> GetByIdAsync(int id)
        {
            return await _context.CategoryOptions
                .FirstOrDefaultAsync(p => p.Id == id) ?? throw new KeyNotFoundException($"Player with id {id} was not found.");
        }
    }
}