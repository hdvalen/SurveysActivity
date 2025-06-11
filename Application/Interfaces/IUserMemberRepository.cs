using Domain.entities;

namespace Application.Interfaces;

public interface IUserMemberRepository : IGenericRepository<UserMember>
{
    Task<UserMember> GetByUsernameAsync(string username);
    Task<UserMember> GetByRefreshTokenAsync(string refreshToken);
}