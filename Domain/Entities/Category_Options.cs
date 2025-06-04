using Domain.entities;

public class CategoryOptions : BaseEntity
{
    public int Id { get; set; }
    public int CatalogOptions_Id { get; set; }
    public int CategoriesOptions_Id { get; set; }
    public DateTime Created_At { get; set; }
    public DateTime Updated_At { get; set; }

    public OptionsResponse? OptionsResponse { get; set; }
    public CategoriesCatalog? CategoriesCatalog { get; set; }
}
