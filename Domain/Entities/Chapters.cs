public class Chapter
{
    public int Id { get; set; }
    public int Survey_Id { get; set; }
    public DateTime Updated_At { get; set; }
    public DateTime Created_At { get; set; }
    public string? ComponentHtml { get; set; }
    public string? ComponentReact { get; set; }
    public string? Chapter_Number { get; set; }
    public string? Chapter_Title { get; set; }

    public Survey? Survey { get; set; }
    public ICollection<Question>? Questions { get; set; }
}
