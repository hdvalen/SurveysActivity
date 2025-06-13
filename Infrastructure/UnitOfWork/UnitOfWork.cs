
using Application.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly SurveyContext _context;
    private ICategoriesCatalogRepository? _categoriesCatalog;
    private ICategoryOptionsRepository? _categoryOptions;
    private IChapterRepository? _chapter;
    private IOptionQuestionsRepository? _optionQuestion;
    private IOptionsResponseRepository? _optionsResponse;
    private IQuestionsRepository? _questions;
    private ISubQuestionsRepository? _subQuestions;
    private ISumaryOptionRepository? _sumaryOption;
    private ISurveyRepository? _survey;
    private IMemberRepository? _UserMember;
    private IRolRepository? _rol;
    private IMemberRolRepository? _memberRol;
    public UnitOfWork(SurveyContext context)
    {
        _context = context;
    }

    public ICategoriesCatalogRepository CategoriesCatalog
    {
        get
        {
            if (_categoriesCatalog == null)
            {
                _categoriesCatalog = new CategoriesCatalogRepository(_context);
            }
            return _categoriesCatalog;
        }
    }

    public ICategoryOptionsRepository CategoryOptions
    {
        get
        {
            if (_categoryOptions == null)
            {
                _categoryOptions = new CategoryOptionsRepository(_context);
            }
            return _categoryOptions;
        }
    }

    public IChapterRepository Chapter
    {
        get
        {
            if (_chapter == null)
            {
                _chapter = new ChapterRepository(_context);
            }
            return _chapter;
        }
    }

    public IOptionQuestionsRepository OptionQuestion
    {
        get
        {
            if (_optionQuestion == null)
            {
                _optionQuestion = new OptionQuestionRepository(_context);
            }
            return _optionQuestion;
        }
    }

    public IOptionsResponseRepository OptionsResponse
    {
        get
        {
            if (_optionsResponse == null)
            {
                _optionsResponse = new OptionsResponseRepository(_context);
            }
            return _optionsResponse;
        }
    }

    public IQuestionsRepository Question
    {
        get
        {
            if (_questions == null)
            {
                _questions = new QuestionsRepository(_context);
            }
            return _questions;
        }
    }

    public ISubQuestionsRepository SubQuestion
    {
        get
        {
            if (_subQuestions == null)
            {
                _subQuestions = new SubQuestionsRepository(_context);
            }
            return _subQuestions;
        }
    }

    public ISumaryOptionRepository SummaryOptions
    {
        get
        {
            if (_sumaryOption == null)
            {
                _sumaryOption = new SumaryOptionReporitory(_context);
            }
            return _sumaryOption;
        }
    }

    public ISurveyRepository Survey
    {
        get
        {
            if (_survey == null)
            {
                _survey = new SurveyReporitory(_context);
            }
            return _survey;
        }
    }
     public IMemberRepository UserMember
    {
        get
        {
            if (_UserMember == null)
            {
                _UserMember = new MemberRepository(_context);
            }
            return _UserMember;
        }
    }
     public IRolRepository Rol
    {
        get
        {
            if (_rol == null)
            {
                _rol = new RolRepository (_context);
            }
            return _rol;
        }
    }
    public IMemberRolRepository MemberRols
    {
        get
        {
            if (_memberRol == null)
            {
                _memberRol = new MemberRolRepository (_context);
            }
            return _memberRol;
        }
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
    }

}