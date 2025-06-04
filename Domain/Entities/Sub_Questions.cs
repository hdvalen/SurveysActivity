public class SubQuestion
{
    public int Id { get; set; }
    public DateTime Created_At { get; set; }
    public DateTime Updated_At { get; set; }
    public int SubQuestion_Id { get; set; }
    public string? SubquestionNumber { get; set; }
    public string? CommentSubquestion { get; set; }
    public string? SubquestionText { get; set; }

    public Question? Question { get; set; }
    public ICollection<OptionQuestion>? OptionQuestions { get; set; }
}
