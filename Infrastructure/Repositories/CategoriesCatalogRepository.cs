
using Application.Interfaces;
using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;


namespace Infrastructure.Repositories
{
    public class CategoriesCatalogRepository : GenericRepository<CategoriesCatalog>, ICategoriesCatalogRepository
    {
        private readonly SurveyContext _context;

        public CategoriesCatalogRepository(SurveyContext context) : base(context)
        {
            _context = context;
        }
         public override async Task<CategoriesCatalog> GetByIdAsync(int id)
        {
            return await _context.CategoriesCatalog
                .FirstOrDefaultAsync(p => p.Id == id) ?? throw new KeyNotFoundException($"Player with id {id} was not found.");
        }
    }
   
}