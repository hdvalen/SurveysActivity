
using Application.Interfaces;
using Domain.entities;

namespace Infrastructure.Repositories
{
    public class SumaryOptionReporitory : GenericRepository<SummaryOptions>, ISumaryOptionRepository
    {
          
    private readonly SurveyContext _context;

        public SumaryOptionReporitory(SurveyContext context) : base(context)
    {
        _context = context;
        
    }
    }
}