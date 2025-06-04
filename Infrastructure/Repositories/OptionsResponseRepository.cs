
using Application.Interfaces;
using Domain.entities;

namespace Infrastructure.Repositories
{
    public class OptionsResponseRepository : GenericRepository<OptionsResponse>, IOptionsResponseRepository
    {
        private readonly SurveyContext _context;

        public OptionsResponseRepository(SurveyContext context) : base(context)
        {
            _context = context;
        }
    }
}