
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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoriesCatalog>> Post(CategoriesCatalog categoriesCatalog)
    {
        _unitOfWork.CategoriesCatalog.Add(categoriesCatalog);
        await _unitOfWork.SaveAsync();
        if (categoriesCatalog == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), new { id = categoriesCatalog.Id }, categoriesCatalog);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(int id, [FromBody] CategoriesCatalog categoriesCatalog)
    {
        if (categoriesCatalog == null)
            return BadRequest("El cuerpo de la solicitud esta vacio.");

        if (id != categoriesCatalog.Id)
            return BadRequest("El Id de la URL no coincide con el del objeto enviado.");

        var existingCategoriesCatalog = await
            _unitOfWork.CategoriesCatalog.GetByIdAsync(id);
        if (existingCategoriesCatalog == null)
            return NotFound($"No se encontro el Catalogo de Categorias con el id {id}.");
        existingCategoriesCatalog.Name = categoriesCatalog.Name;
        _unitOfWork.CategoriesCatalog.Update(existingCategoriesCatalog);

        await _unitOfWork.SaveAsync();

        return Ok(existingCategoriesCatalog);
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