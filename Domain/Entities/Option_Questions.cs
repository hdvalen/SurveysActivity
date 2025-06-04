public class OptionQuestion
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

    public SubQuestion? SubQuestion { get; set; }
    public CategoriesCatalog? CategoriesCatalog { get; set; }
    public Question? Question { get; set; }
    public OptionsResponse? OptionsResponse { get; set; }
   
}
