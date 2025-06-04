using Application.Interfaces;
using Domain.entities;

namespace Infrastructure.Repositories
{
    public class SubQuestionsRepository : GenericRepository<SubQuestion>, ISubQuestionsRepository
    {
          
    private readonly SurveyContext _context;

        public SubQuestionsRepository(SurveyContext context) : base(context)
        {
            _context = context;

        }
    }
}