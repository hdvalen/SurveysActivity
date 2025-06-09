
using Application.Interfaces;
using AutoMapper;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiSurveys.Controllers;

public class CategoriesCatalogController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CategoriesCatalogController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<CategoriesCatalogDto>>>
    Get()
    {
        var categoriesCatalog = await
        _unitOfWork.CategoriesCatalog.GetAllAsync();
        return _mapper.Map<List<CategoriesCatalogDto>>(categoriesCatalog);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoriesCatalogDto>> Get(int id)
    {
        var categoriesCatalog = await
        _unitOfWork.CategoriesCatalog.GetByIdAsync(id);
        if (categoriesCatalog == null)
        {
            return NotFound($"Categories Catalog with id {id} was not found");
        }
        return _mapper.Map<CategoriesCatalogDto>(categoriesCatalog);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoriesCatalog>> Post(CategoriesCatalogDto categoriesCatalogDto)
    {
        var CategoriesCatalog = _mapper.Map<CategoriesCatalog>(categoriesCatalogDto);
        _unitOfWork.CategoriesCatalog.Add(CategoriesCatalog);
        await _unitOfWork.SaveAsync();
        if (categoriesCatalogDto == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), new { id = categoriesCatalogDto.Id }, categoriesCatalogDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(int id, [FromBody] CategoriesCatalogDto categoriesCatalogDto)
    {
        if (categoriesCatalogDto == null)
            return NotFound();

        var CategoriesCatalog = _mapper.Map<CategoriesCatalog>(categoriesCatalogDto);
        _unitOfWork.CategoriesCatalog.Update(CategoriesCatalog);

        await _unitOfWork.SaveAsync();

        return Ok(categoriesCatalogDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var categoriesCatalog = await _unitOfWork.CategoriesCatalog.GetByIdAsync(id);
        if (categoriesCatalog == null)
            return NotFound();

        _unitOfWork.CategoriesCatalog.Remove(categoriesCatalog);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }   

}