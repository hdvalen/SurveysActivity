public class CategoriesCatalog
{
    public int Id { get; set; }
    public DateTime Created_At { get; set; }
    public DateTime Updated_At { get; set; }
    public string? Name { get; set; }

    // Un CategoriesCatalog tiene muchos CategoryOptions y OptionQuestions
    public ICollection<CategoryOptions>? CategoryOptions { get; set; }
    public ICollection<OptionQuestion>? OptionQuestions { get; set; }
}
