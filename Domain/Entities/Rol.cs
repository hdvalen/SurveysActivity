namespace Domain.entities;

public class Rol : BaseEntity
{
    public int Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public ICollection<MemberRols> MembersRols { get; set; } = new HashSet<MemberRols>();
}