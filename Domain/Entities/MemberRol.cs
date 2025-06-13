namespace Domain.entities;

public class MemberRols : BaseEntity
{
    public int MemberId { get; set; }
    public UserMember? Member { get; set; }
    public int RolId { get; set; } 
    public Rol? Rol { get; set; }
    
}