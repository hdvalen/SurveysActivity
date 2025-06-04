
using Application.Interfaces;
using Domain.entities;

namespace Infrastructure.Repositories
{
    public class OptionQuestionRepository : GenericRepository<OptionQuestion>, IOptionQuestionsRepository
    {
        private readonly SurveyContext _context;

        public OptionQuestionRepository(SurveyContext context) : base(context)
        {
            _context = context;
        }
    }
}