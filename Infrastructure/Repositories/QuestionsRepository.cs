using Application.Interfaces;
using Domain.entities;

namespace Infrastructure.Repositories
{
    public class QuestionsRepository : GenericRepository<Question>, IQuestionsRepository
    {
        private readonly SurveyContext _context;

        public QuestionsRepository(SurveyContext context) : base(context)
        {
            _context = context;
        }
    }
}