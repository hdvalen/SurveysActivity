namespace Application.DTOs;

public class OptionsResponseDto
{
    public long Id { get; set; }
    public long OptionQuestionId { get; set; }
    public DateTime? Created_At { get; set; }
    public DateTime? Updated_At { get; set; }
    public string? OptionText { get; set; }
}