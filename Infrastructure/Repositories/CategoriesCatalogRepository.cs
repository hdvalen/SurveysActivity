
using Application.Interfaces;
using Domain.entities;

namespace Infrastructure.Repositories
{
    public class CategoriesCatalogRepository : GenericRepository<CategoriesCatalog>, ICategoriesCatalogRepository
    {
        private readonly SurveyContext _context;

        public CategoriesCatalogRepository(SurveyContext context) : base(context)
        {
            _context = context;
        }
    }
}