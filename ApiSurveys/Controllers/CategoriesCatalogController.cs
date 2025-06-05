
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiSurveys.Controllers;

public class CategoriesCatalogController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    public CategoriesCatalogController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<CategoriesCatalog>>>                                 
    Get()
    {
        var categoriesCatalog = await
        _unitOfWork.CategoriesCatalog.GetAllAsync();
        return Ok(categoriesCatalog);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int id)
    {
        var categoriesCatalog = await
        _unitOfWork.CategoriesCatalog.GetByIdAsync(id);
        if (categoriesCatalog == null)
        {
            return NotFound($"Categories Catalog with id {id} was not found");
        }
        return Ok(categoriesCatalog);
    }
}