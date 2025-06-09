namespace Application.DTOs;

public class ChaptersDto
{
    public int Id { get; set; }
    public int Survey_Id { get; set; }
    public DateTime Updated_At { get; set; }
    public DateTime Created_At { get; set; }
    public string? ComponentHtml { get; set; }
    public string? ComponentReact { get; set; }
    public string? Chapter_Number { get; set; }
    public string? Chapter_Title { get; set; }
}