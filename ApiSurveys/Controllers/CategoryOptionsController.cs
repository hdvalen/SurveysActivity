using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.entities;

namespace ApiSurveys.Controllers;

public class CategoryOptionsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    public CategoryOptionsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CategoryOptions>>>
     Get()
    {
        var categoryOptions = await _unitOfWork.CategoryOptions.GetAllAsync();
        return Ok(categoryOptions);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryOptions>>
    Get(int id)
    {
        var categoryOption = await _unitOfWork.CategoryOptions.GetByIdAsync(id);
        if (categoryOption == null)
            return NotFound($"Category Options with id {id} was not found");
        return Ok(categoryOption);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoryOptions>> Post(CategoryOptions categoryOptions)
    {
        _unitOfWork.CategoryOptions.Add(categoryOptions);
        await _unitOfWork.SaveAsync();
         if (categoryOptions == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), new { id = categoryOptions.Id }, categoryOptions);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] CategoryOptions categoryOptions)
    {
        if (categoryOptions == null)
            return BadRequest("El cuerpo de la solicitud esta vacio.");

        if (id != categoryOptions.Id)
            return BadRequest("El Id de la URL no coincide con el del objeto enviado.");

        var existingCategoryOption = await
         _unitOfWork.CategoryOptions.GetByIdAsync(id);
        if (existingCategoryOption == null)
            return NotFound($"No se encontro category option con id {id}");

        // Actualiza las propiedades necesarias
        existingCategoryOption.CatalogOptions_Id = categoryOptions.CatalogOptions_Id;
        existingCategoryOption.CategoriesOptions_Id = categoryOptions.CategoriesOptions_Id;
        existingCategoryOption.Updated_At = DateTime.UtcNow;
        // ...otros campos...

        _unitOfWork.CategoryOptions.Update(existingCategoryOption);
        await _unitOfWork.SaveAsync();

        return Ok(existingCategoryOption);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var categoryOptions = await _unitOfWork.CategoryOptions.GetByIdAsync(id);
        if (categoryOptions == null)
            return NotFound();

        _unitOfWork.CategoryOptions.Remove(categoryOptions);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}