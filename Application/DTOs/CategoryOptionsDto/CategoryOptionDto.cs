namespace Application.DTOs;

public class CategoryOptionDto
{
    public int Id { get; set; }
    public int CatalogOptions_Id { get; set; }
    public int CategoriesOptions_Id { get; set; }
    public DateTime Created_At { get; set; }
    public DateTime Updated_At { get; set; }
}