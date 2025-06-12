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
    IMemberRepository Member { get; }
    IRolRepository Rol { get; }
    IMemberRolRepository MemberRols { get; }
    Task<int> SaveAsync();


}