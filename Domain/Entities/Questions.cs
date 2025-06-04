using Domain.entities;

public class Question : BaseEntity
{
    public int Id { get; set; }
    public int ChapterId { get; set; }
    public DateTime Created_At { get; set; }
    public DateTime Updated_At { get; set; }
    public string? Question_Number { get; set; }
    public string? Response_Type { get; set; }
    public string? Comment_Question { get; set; }
    public string? Question_Text { get; set; }

    public Chapter? Chapter { get; set; }
    public ICollection<SubQuestion>? SubQuestions { get; set; }
    public ICollection<OptionQuestion>? OptionQuestions { get; set; }
    public ICollection<SummaryOptions>? SummaryOptions { get; set; }
}
