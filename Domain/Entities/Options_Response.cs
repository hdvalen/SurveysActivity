using Domain.entities;

public class OptionsResponse : BaseEntity
{
    public long Id { get; set; }
    public long OptionQuestionId { get; set; }
    public DateTime? Created_At { get; set; }
    public DateTime? Updated_At { get; set; }
    public string? OptionText { get; set; }

     public ICollection<OptionQuestion>? OptionQuestions { get; set; }
     public ICollection<CategoryOptions>? CategoryOptions { get; set; }
}
