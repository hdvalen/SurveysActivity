using Domain.entities;

public class Survey : BaseEntity
{
    public int Id { get; set; }
    public DateTime Created_At { get; set; }
    public DateTime Updated_At { get; set; }
    public string? ComponentHtml { get; set; }
    public string? ComponentReact { get; set; }
    public string? Description { get; set; }
    public string? Instruction { get; set; }
    public string? Name { get; set; }

    public ICollection<Chapter>? Chapters { get; set; }
    public ICollection<SummaryOptions>? SummaryOptions { get; set; }
}
