using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.entities;

namespace ApiSurveys.Controllers;

public class CategoryOptionsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CategoryOptionsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CategoryOptionDto>>>
     Get()
    {
        var categoryOptions = await _unitOfWork.CategoryOptions.GetAllAsync();
        return _mapper.Map<List<CategoryOptionDto>>(categoryOptions);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryOptionDto>>
    Get(int id)
    {
        var categoryOption = await _unitOfWork.CategoryOptions.GetByIdAsync(id);
        if (categoryOption == null)
            return NotFound($"Category Options with id {id} was not found");
        return _mapper.Map<CategoryOptionDto>(categoryOption);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoryOptions>> Post(CategoryOptionDto categoryOptionsDto)
    {
        var CategoryOptions = _mapper.Map<CategoryOptions>(categoryOptionsDto);
        _unitOfWork.CategoryOptions.Add(CategoryOptions);
        await _unitOfWork.SaveAsync();
         if (categoryOptionsDto == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), new { id = categoryOptionsDto.Id }, categoryOptionsDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] CategoryOptionDto categoryOptionsDto)
    {
        if (categoryOptionsDto == null)
            return BadRequest("El cuerpo de la solicitud esta vacio.");

        var CategoryOptions = _mapper.Map<CategoryOptions>(categoryOptionsDto);

        _unitOfWork.CategoryOptions.Update(CategoryOptions);
        await _unitOfWork.SaveAsync();

        return Ok(categoryOptionsDto);
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