using Application.DTOs;
using AutoMapper;

namespace ApiSurveys.Profiles;

public class MappingPorfiles : Profile
{
    public MappingPorfiles()
    {
        CreateMap<CategoriesCatalog, CategoriesCatalogDto>().ReverseMap();
        CreateMap<CategoryOptions, CategoryOptionDto>().ReverseMap();
        CreateMap<Chapter, ChaptersDto>().ReverseMap();
        CreateMap<OptionQuestion, OptionsQuestionsDto>().ReverseMap();
        CreateMap<OptionsResponse, OptionsResponseDto>().ReverseMap();
        CreateMap<Question, QuestionsDto>().ReverseMap();
        CreateMap<SubQuestion, SubQuestionDto>().ReverseMap();
        CreateMap<SummaryOptions, SumaryOptionDto>().ReverseMap();
        CreateMap<Survey, SurveyDto>().ReverseMap();
    }
}