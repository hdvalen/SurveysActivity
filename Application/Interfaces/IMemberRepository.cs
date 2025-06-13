using Domain.entities;

namespace Application.Interfaces;

public interface IMemberRepository : IGenericRepository<UserMember>
{
    Task<UserMember> GetByUsernameAsync(string username);
    Task<UserMember> GetByRefreshTokenAsync(string refreshToken);
    
}