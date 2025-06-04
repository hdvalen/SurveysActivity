
using Application.Interfaces;
using Domain.entities;

namespace Infrastructure.Repositories
{
    public class SurveyReporitory : GenericRepository<Survey>, ISurveyRepository
    {
          
    private readonly SurveyContext _context;

        public SurveyReporitory(SurveyContext context) : base(context)
    {
        _context = context;
        
    }
    }
}