using Application.Interfaces; 
using Domain.entities; 
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories; 
public class MemberRepository : GenericRepository<Member>, IMemberRepository { 
    private readonly SurveyContext _context; 
    public MemberRepository(SurveyContext context) : base(context) {}
    public async Task<Member> GetByUsernameAsync(string username)
        {
            return await _context.Members
                .Include(u => u.MemberRols)
                    .ThenInclude(mr => mr.Rol)
                .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }
} 