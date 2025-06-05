namespace Application.Interfaces;

public interface IUnitOfWork
{
    ICategoriesCatalogRepository CategoriesCatalog { get; }
    ICategoryOptionsRepository CategoryOptions { get; }
    IChapterRepository Chapter { get; }
    IOptionQuestionsRepository OptionQuestion { get; }
    IOptionsResponseRepository OptionsResponse { get; }
    IQuestionsRepository Question { get; }
    ISubQuestionsRepository SubQuestion { get; }
    ISumaryOptionRepository SummaryOptions { get; }
    ISurveyRepository Survey { get; }
    Task<int> SaveAsync();


}