
using Application.Interfaces;
using Domain.entities;

namespace Infrastructure.Repositories
{
    public class CategoryOptionsRepository : GenericRepository<CategoryOptions>, ICategoryOptionsRepository
    {
        private readonly SurveyContext _context;

        public CategoryOptionsRepository(SurveyContext context) : base(context)
        {
            _context = context;
        }
    }
}