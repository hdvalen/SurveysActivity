namespace Domain.entities;

public abstract class BaseEntity
{
    public DateTime createdAt { get; set; }
    public DateTime? updateAt { get; set; }
}