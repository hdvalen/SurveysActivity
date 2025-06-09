namespace Application.DTOs;

public class OptionsQuestionsDto
{
    public int Id { get; set; }
    public int Option_Id { get; set; }
    public int SubQuestion_Id { get; set; }
    public int OptionCatalog_Id { get; set; }
    public int OptionQuestion_Id { get; set; }
    public DateTime Created_At { get; set; }
    public DateTime Updated_At { get; set; }
    public string? CommentOptiones { get; set; }
    public string? NumberOption { get; set; }
}