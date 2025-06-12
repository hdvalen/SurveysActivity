using Application.Interfaces; 
using Domain.entities; 
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;



namespace Infrastructure.Repositories;

public class MemberRolRepository : GenericRepository<MemberRols>, IMemberRolRepository
{
    private readonly SurveyContext _context;
    public MemberRolRepository(SurveyContext context) : base(context)
    {
        _context = context;
    } 
    
    public override async Task<MemberRols> GetByIdAsync(int id)
        {
            return await _context.MemberRols
                .FirstOrDefaultAsync(mr => mr.MemberId == id) ?? throw new KeyNotFoundException($"Member Rols with id {id} was not found");
        }
} 