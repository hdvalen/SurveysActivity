namespace Application.DTOs;

public class SumaryOptionDto
{
    public int Id { get; set; }
    public int Survey_Id { get; set; }
    public string? Code_Number { get; set; }
    public int Question_Id { get; set; }
    public string? ValueRta { get; set; }
}