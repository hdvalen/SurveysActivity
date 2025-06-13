using Application.Interfaces; 
using Domain.entities; 
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories; 
public class MemberRepository : GenericRepository<UserMember>, IMemberRepository { 
    private readonly SurveyContext _context; 
    public MemberRepository(SurveyContext context) : base(context) {}

    public async Task<UserMember> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Members
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(rt => rt.Token == refreshToken));
    }

    public async Task<UserMember> GetByUsernameAsync(string username)
        {
            return await _context.Members
                .Include(u => u.MemberRols)
                    .ThenInclude(mr => mr.Rol)
                .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }
} 