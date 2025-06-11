
namespace Domain.entities;

public class RefreshToken : BaseEntity
{
    public int Id { get; set; }
    public string? MemberId { get; set; }
    public UserMember? Users { get; set; }
    public string? Token { get; set; }
    public DateTime Expires { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public DateTime Created { get; set; }
    public DateTime? Revoked { get; set; }
    public bool IsActive => Revoked == null && !IsExpired;
}