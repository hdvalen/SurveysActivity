namespace Domain.entities;

public class MemberRols : BaseEntity
{
    public string? MemberId { get; set; }
    public UserMember? UserMember { get; set; }
    public int RolId { get; set; } 
    public Rol? Rol { get; set; }
    
}