using Domain.entities;

namespace Application.Interfaces;
public interface IMemberRepository : IGenericRepository<Member>
{
    Task<Member> GetByUsernameAsync(string username);
}